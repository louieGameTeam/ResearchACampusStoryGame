# Written by Kevin Chao
# Last Updated Mar. 11th, 2023

from FirebaseInterface import ReSearchDatabase
from dataclasses import dataclass
from datetime import datetime
from dateutil import parser, tz
from os.path import exists
import json, csv, pandas

# Easy access constants/variables to prevent user input errors

# Filenames
# ALL RELEVANT INPUT AND OUTPUT FILES SHOULD BE IN THE SAME DIRECTORY AS THE SCRIPT FOR AN EASIER LIFE
GRADES_INPUT_CSV = '2023 WQ Grades.csv'
ROSTER_XLS_LIST = ['Section 1 Roster.xls', 'Section 2 Roster.xls']
DATABASE_JSON = 'researchacampusstory-default-rtdb-export.json'
GRADES_OUTPUT_CSV = 'BIM 088V 001 WQ2023 UpdatedGrades.csv'

# Column headers in roster
ROSTER_STUDENT_ID = 'Student ID'
ROSTER_LAST_NAME = 'Last Name'
ROSTER_FIRST_NAME = 'First Name'
ROSTER_EMAIL = 'Email'

# Column headers to be used in output
COLUMN_HEADERS = []

# Firebase database fields
STUDENTS = 'students'
SCHEDULE = 'schedule'
DATES = 'dates'
DUE_DATES = 'dueDates'
DATA = 'data'
LOG = 'log'
LAST_PROGRESS = 'lastProgress'
PROGRESS = 'progress'
USER_INFO = 'userInfo'
EMAIL = 'email'
FIRST_NAME = 'firstName'
LAST_NAME = 'lastName'

DEFAULT = 'default'
# Additional entries may need to be added to this dict if more unexpected date/time strings appear in database input
# This may become unnecessary in the future if the data saved to the database is made uniform with a UTC timezone format
TZ_INFOS = {DEFAULT: tz.gettz('Pacific Standard Time'), 'SA': tz.gettz('Eastern Standard Time'), 'CH': tz.gettz('China Standard Time')}

# Saved to variable to remain fixed during runtime
CUR_DATE_TIME = datetime.now(tz = TZ_INFOS[DEFAULT])

# Current Date/Time override if grades are provided late
CUR_DATE_TIME = CUR_DATE_TIME.replace(day = 7)



# Struct definition to store player data easier
@dataclass
class PlayerInfo:
    studentName: tuple[str, str]
    studentID: int
    userID: int
    loginID: str
    section: str
    grades: list[int]

    def __eq__(self, obj):
        return isinstance(obj, PlayerInfo) and self.studentName == obj.studentName and self.userID == obj.userID and self.loginID == obj.loginID and self.section == obj.section

    def __repr__(self):
        output = f'\nSTUDENT NAME: {self.studentName}'
        output += f'\nSTUDENT ID: {self.studentID}'
        output += f'\nUSER ID: {self.userID}'
        output += f'\nLOGIN ID: {self.loginID}'
        output += f'\nSECTION: {self.section}\n'

        return output

    def Unpack(self) -> list:
        return [', '.join(self.studentName), self.studentID, self.userID, self.loginID, self.section] + self.grades


# Helper Functions
def ConstructRoster(rosterList: list[str]) -> dict[int: str]:
    newRoster = dict()

    for rosterName in rosterList:
        rosterDataFrame = pandas.read_excel(rosterName, usecols = [0, 5], header = 8)

        for i in range(0, len(rosterDataFrame)):
            curStudentID = rosterDataFrame[ROSTER_STUDENT_ID][i]
            curEmail = rosterDataFrame[ROSTER_EMAIL][i]

            newRoster[curStudentID] = curEmail

    return newRoster



# Deprecate these two
def ConstructReleaseDateDict(gameFields: dict, numLevels: int) -> dict[int: datetime]:
    output = dict()

    for levelNum in range(numLevels):
        parse = parser.parse(' '.join(gameFields[SCHEDULE][DATES][levelNum].split(' ')[0:-1]))

        if parse.tzinfo == None:
            parse = parse.replace(tzinfo = TZ_INFOS[DEFAULT])

        output[levelNum] = parse

    return output

def ConstructDueDateDict(gameFields: dict, numLevels: int) -> dict[int: datetime]:
    output = dict()

    for levelNum in range(numLevels):
        parse = parser.parse(' '.join(gameFields[SCHEDULE][DUE_DATES][levelNum].split(' ')[0:-1]))

        if parse.tzinfo == None:
            parse = parse.replace(tzinfo = TZ_INFOS[DEFAULT])

        output[levelNum] = parse

    return output



# May need to revisit helper function tzinfo once Unity code refactored
def ConstructGradeList(levels: dict, curDateTime: datetime, releaseDateDict: dict, dueDateDict: dict) -> list[int]:
    def CleanLastProgress(lastProgress: str) -> datetime:
        # Contingencies for handling improper datetime formats, assumes most timezones are going to be in Pacific Standard Time
        lastProgress = lastProgress.replace('-', '/')

        try:
            parsed = parser.parse(lastProgress, tzinfos = TZ_INFOS)

            if parsed.tzinfo == None:
                return parsed.replace(tzinfo = TZ_INFOS[DEFAULT])

            return parsed

        except parser.ParserError:
            isPM = False
            if 'PM' in lastProgress:
                isPM = True
                newInput = lastProgress.replace('PM', '')
            elif 'AM' in lastProgress:
                newInput = lastProgress.replace('AM', '')

            newParse = parser.parse(newInput, tzinfos = TZ_INFOS)
            if newParse.tzinfo == None:
                newParse = newParse.replace(tzinfo = TZ_INFOS[DEFAULT])
            if isPM and newParse.hour < 12:
                return newParse.replace(hour = newParse.hour + 12)

            return newParse


    progressList = list()

    for levelNum, levelFields in levels.items():
        # If there is progress data
        if levelFields[LAST_PROGRESS] != '':
            # Clean LAST_PROGRESS format to be usable in comparison against due dates
            lastProgress = CleanLastProgress(levelFields[LAST_PROGRESS])

            # If the level was completed before the due date
            if lastProgress <= dueDateDict[levelNum] and levelFields[PROGRESS] == 1:
                progressList.append(1)

            # Otherwise, the level was completed after the due date or was not completed
            else:
                progressList.append(0)

        # If there is no progress data
        else:
            # If the level has not released yet or the level has been released and the due date has not yet passed
            if curDateTime < releaseDateDict[levelNum] or curDateTime < dueDateDict[levelNum]:
                progressList.append(None)

            # Otherwise, if the level has been released and the due date has passed
            else:
                progressList.append(0)

    return progressList

# Deprecate
def ConstructDatabaseGradeDict(gameFields: dict) -> dict[tuple[tuple, str]: list[int]]:
    newDatabaseGradeDict = dict()

    # Process database data and create updated grade dict
    for student in gameFields[STUDENTS]:
        studentFields = gameFields[STUDENTS][student]

        # Grabbing player name for gradeDict keys
        studentData = studentFields[DATA]
        curPlayerName = (studentData[USER_INFO][LAST_NAME].strip(' '), studentData[USER_INFO][FIRST_NAME].strip(' '))

        # Grabbing player email for gradeDict keys
        curPlayerEmail = studentData[USER_INFO][EMAIL].lower()

        # Grabbing grades list for gradeDict values
        studentProgress = studentFields[PROGRESS]
        numLevels = len(studentProgress[LOG])
        releaseDateDict = ConstructReleaseDateDict(gameFields, numLevels)
        dueDateDict = ConstructDueDateDict(gameFields, numLevels)
        levels = {levelNum: studentProgress[LOG][levelNum] for levelNum in range(0, numLevels)}
        curPlayerGrades = ConstructGradeList(levels, CUR_DATE_TIME, releaseDateDict, dueDateDict)

        newDatabaseGradeDict[(curPlayerName, curPlayerEmail)] = curPlayerGrades

    return newDatabaseGradeDict



def ConstructLoadedGradeDict(csvReader: csv.reader) -> dict[int: PlayerInfo]:
    missingFromRoster = set()
    missingSISUserID = set()
    newLoadedGradeDict = dict()

    for line in csvReader:
        # When student SIS User IDs are missing for some reason
        if line[2] == '':
            missingSISUserID.add((line[0]))
            continue

        # Only load player grades for students that are still in the roster, determined by presence of SIS User ID in roster
        if int(line[2]) in playerRoster.keys():
            studentName = tuple(line[0].split(', '))
            studentID = int(line[1])
            userID = int(line[2])
            loginID = line[3]
            section = line[4]
            grades = line[5:15]
            curPlayer = PlayerInfo(studentName, studentID, userID, loginID, section, grades)

            newLoadedGradeDict[curPlayer.userID] = curPlayer

        # Keep track of set of students that are no longer in roster for display
        else:
            missingFromRoster.add((line[0], line[2]))

    # If there are students that are in the loaded gradebook that are no longer in the roster, display them to console
    if len(missingFromRoster) > 0:
        print('===== STUDENTS MISSING FROM ROSTER =====')
        for student in sorted(missingFromRoster, key = lambda x: tuple(x[0].split(', '))):
            print(student)
        print('========================================\n')

    # If there are students in the loaded gradebook that have no SIS User IDs, display them to console
    if len(missingSISUserID) > 0:
        print('===== STUDENTS MISSING SIS USER ID =====')
        for student in sorted(missingSISUserID, key = lambda x: tuple(x[0].split(', '))):
            print(student)

        print('========================================\n')

    return newLoadedGradeDict

def UpdateLoadedGradeDict(loadedGradeDict: dict, playerRoster: dict, databaseGradeDict: dict) -> dict[int: PlayerInfo]:
    newLoadedGradeDict = loadedGradeDict.copy()

    # If there were player grades loaded, update the loadedGradeDict with new database data
    if len(loadedGradeDict) > 0:
        # Set of players in the loadedGradeDict whose emails don't match any in the database
        unmatchedPlayers = set()

        for userID, playerInfo in loadedGradeDict.items():
            # Construct playerKey in the format ((LastName, FirstName), Email)
            playerKey = (playerInfo.studentName, playerRoster[playerInfo.userID])

            # Keep track of players from the loadedGradeDict that are not in the database
            if playerKey not in databaseGradeDict.keys():
                unmatchedPlayers.add((', '.join(playerKey[0]), playerKey[1]))

            # If player can be matched to database data, then update grades in loadedGradeDict
            else:
                for i in range(1, len(playerInfo.grades)):
                    # If grade for level i is not already stored in loaded grades, update with grade from database
                    if playerInfo.grades[i] in [None, '']:
                        newLoadedGradeDict[userID].grades[i] = databaseGradeDict[playerKey][i - 1]

        # Display set of players whose loadedGradeDict emails don't match database emails
        if len(unmatchedPlayers) > 0:
            print('===== STUDENTS WITH MISMATCHED EMAILS =====')
            for student in sorted(unmatchedPlayers, key = lambda x: tuple(x[0].split(', '))):
                print(student)
            print('===========================================\n')

    return newLoadedGradeDict



# Keys are ROSTER Student IDs, values are emails, ideally used to tie database emails to Student ID/SIS User ID
playerRoster: dict[int: str] = dict()

# Keys are a nested tuple in the format ((LastName, FirstName), Email), values are lists of level grades
databaseGradeDict: dict[tuple[tuple, str]: list[int]] = dict()

# Keys are player's Student IDs/SIS User IDs, values are stored PlayerInfo objects
loadedGradeDict: dict[int: PlayerInfo] = dict()

# Load roster from .xls files
playerRoster = ConstructRoster(ROSTER_XLS_LIST)


# Convert to using the Firebase Interface
# Load database instance data
# database = ReSearchDatabase(CUR_DATE_TIME)

# Load database data from .json file
if exists(DATABASE_JSON):
    with open(DATABASE_JSON, 'r') as jsonFile:
        gameFields = json.load(jsonFile)
        databaseGradeDict = ConstructDatabaseGradeDict(gameFields)

else:
    raise Exception(f'Missing expected file: {DATABASE_JSON}')


# Load existing grades from .csv file
if exists(GRADES_INPUT_CSV):
    with open(GRADES_INPUT_CSV, 'r') as inputCSVFile:
        csvReader = csv.reader(inputCSVFile)
        COLUMN_HEADERS = csvReader.__next__()[0:15] # Grab column headers
        csvReader.__next__() # Skip next row of column headers, thanks Canvas
        loadedGradeDict = ConstructLoadedGradeDict(csvReader)

    # Update the loadedGradeDict with database data
    loadedGradeDict = UpdateLoadedGradeDict(loadedGradeDict, playerRoster, databaseGradeDict)

else:
    raise Exception(f'Missing expected file: {GRADES_INPUT_CSV}')


# Write new .csv
with open(GRADES_OUTPUT_CSV, 'w', newline = '') as outputGradesCSV:
    writer = csv.writer(outputGradesCSV)
    writer.writerow(COLUMN_HEADERS)

    # Sorted by tuples of (LastName, FirstName), this preserves order of the original gradebook
    for player in sorted(loadedGradeDict.values(), key = lambda x: x.studentName):
        writer.writerow(player.Unpack())