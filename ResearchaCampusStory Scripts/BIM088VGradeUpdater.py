# Written by Kevin Chao
# Last Updated June 18th, 2023
#
# This grading script is meant to be used as a mostly-automated grading tool.
# Instructions for its use can be found at:
# https://docs.google.com/document/d/1AgAyvmqwWCodcsrlkgqrolq8TQoxTsO4Y8akFTXkPn0/edit?usp=sharing

from FirebaseInterface import ReSearchDatabase
from dataclasses import dataclass
from datetime import datetime
from os.path import exists
import csv, pandas

# Easy access constants/variables to prevent user input errors
# Filenames
#   ALL RELEVANT INPUT AND OUTPUT FILES SHOULD BE IN THE SAME DIRECTORY AS THE SCRIPT FOR AN EASIER LIFE

# Filename for the Canvas gradebook the professor provides
GRADES_INPUT_CSV = '2023 SQ Grades.csv'

# List of filenames for the student rosters the professor provides
ROSTER_XLS_LIST = ['Section 1 Roster.xls', 'Section 2 Roster.xls']

# Filename for the .csv that will be output that contains student grade information
GRADES_OUTPUT_CSV = 'BIM 088V SQ2023 UpdatedGrades.csv'

# Column headers in roster
ROSTER_STUDENT_ID = 'Student ID'
ROSTER_LAST_NAME = 'Last Name'
ROSTER_FIRST_NAME = 'First Name'
ROSTER_EMAIL = 'Email'

# Variable to store column headers from Canvas gradebook to be used in output later
COLUMN_HEADERS = []

# Saved to variable to remain fixed during runtime, script assumed to be run from a machine using Pacific Standard Time
CUR_DATE_TIME = datetime.now()

# Pull data from database, stored in ReSearchDatabase object
DATABASE = ReSearchDatabase(CUR_DATE_TIME)



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
# Constructs a student roster represented as a dictionary containing the sum of all
# students from each section that a .xls roster exists for, as stored in ROSTER_XLS_LIST
def ConstructRoster(rosterList: list[str]) -> dict[int: str]:
    newRoster = dict()

    for rosterName in rosterList:
        if exists(rosterName):
            rosterDataFrame = pandas.read_excel(rosterName, usecols = [0, 5], header = 8)

            for i in range(0, len(rosterDataFrame)):
                curStudentID = rosterDataFrame[ROSTER_STUDENT_ID][i]
                curEmail = rosterDataFrame[ROSTER_EMAIL][i]

                newRoster[curStudentID] = curEmail

        else:
            raise Exception(f'Missing expected file: {rosterName}')

    return newRoster


# Constructs a dictionary from the Canvas gradebook where keys are a student's SIS
# User ID and values are a PlayerInfo object representing that student
def LoadStudentDict(csvReader: csv.reader) -> dict[int: PlayerInfo]:
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


# Updates the loadedStudentDict with grade info pulled from the database, returns
# new loadedStudentDict with updated PlayerInfo
def UpdateLoadedStudentDict(loadedStudentDict: dict, playerRoster: dict, databaseGradeDict: dict) -> dict[int: PlayerInfo]:
    newLoadedStudentDict = loadedStudentDict.copy()

    # If there were player grades loaded, update the loadedStudentDict with new database data
    if len(loadedStudentDict) > 0:
        # Set of players in the loadedStudentDict whose emails don't match any in the database
        unmatchedPlayers = set()

        for userID, playerInfo in loadedStudentDict.items():
            # Construct playerKey in the format ((LastName, FirstName), Email)
            playerKey = (playerInfo.studentName, playerRoster[playerInfo.userID])

            # Keep track of players from the loadedStudentDict that are not in the database
            if playerKey not in databaseGradeDict.keys():
                unmatchedPlayers.add((', '.join(playerKey[0]), playerKey[1]))

            # If player can be matched to database data, then update grades in loadedStudentDict
            else:
                for i in range(1, len(playerInfo.grades)):
                    # If grade for level i is not already stored in loaded grades, update with grade from database
                    if playerInfo.grades[i] in [None, '']:
                        newLoadedStudentDict[userID].grades[i] = databaseGradeDict[playerKey][i - 1]

        # Display set of players whose loadedStudentDict emails don't match database emails
        if len(unmatchedPlayers) > 0:
            print('===== STUDENTS WITH MISMATCHED EMAILS =====')
            for student in sorted(unmatchedPlayers, key = lambda x: tuple(x[0].split(', '))):
                print(student)
            print('===========================================\n')

    return newLoadedStudentDict



if (__name__ == '__main__'):
    # Keys are ROSTER Student IDs, values are emails, ideally used to tie database emails to Student ID/SIS User ID
    playerRoster: dict[int: str] = dict()

    # Keys are player's Student IDs/SIS User IDs, values are stored PlayerInfo objects
    loadedStudentDict: dict[int: PlayerInfo] = dict()

    # Load roster from .xls files
    playerRoster = ConstructRoster(ROSTER_XLS_LIST)



    # Load existing Canvas gradebook from .csv file
    if exists(GRADES_INPUT_CSV):
        with open(GRADES_INPUT_CSV, 'r') as inputCSVFile:
            csvReader = csv.reader(inputCSVFile)
            COLUMN_HEADERS = csvReader.__next__()[0:15] # Grab column headers
            csvReader.__next__() # Skip next row of column headers, thanks Canvas
            csvReader.__next__() # Skip Points header (Possible header, wasn't a problem in WQ2023 but was in SQ2023)
            loadedStudentDict = LoadStudentDict(csvReader)

        # Update the loadedStudentDict with database data
        loadedStudentDict = UpdateLoadedStudentDict(loadedStudentDict, playerRoster, DATABASE.gradeDict)

    else:
        raise Exception(f'Missing expected file: {GRADES_INPUT_CSV}')

    # Write new output .csv
    with open(GRADES_OUTPUT_CSV, 'w', newline = '') as outputGradesCSV:
        writer = csv.writer(outputGradesCSV)
        writer.writerow(COLUMN_HEADERS)

        # Sorted by tuples of (LastName, FirstName), this preserves order of the original gradebook
        for player in sorted(loadedStudentDict.values(), key = lambda x: x.studentName):
            writer.writerow(player.Unpack())