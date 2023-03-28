# Written by Kevin Chao
# Last Updated Mar. 27th, 2023
#
# Utility script to be run at the beginning of a new quarter to delete
# all student data from previous quarters. Script should be run from
# the game TA's local machine.
#
# https://firebase.google.com/docs/auth/admin/manage-users#python

from firebase_admin import auth, db, credentials, initialize_app

# Constants
# Filename for auth key
PRIVATE_KEY = 'researchacampusstory-firebase-adminsdk-fxrri-797ae69b9c.json'
DATABASE_URL = 'https://researchacampusstory-default-rtdb.firebaseio.com/'

# Student database directory name
STUDENTS = '/students'



# Initialization
initialize_app(credentials.Certificate(PRIVATE_KEY), {'databaseURL': DATABASE_URL})

# Student database reference
ref = db.reference(STUDENTS)

# Clear Authentication page
# Will store a list of user UIDs for use in modifying the database
usersToModify = [user.uid for user in auth.list_users().iterate_all()]

deletionResult = auth.delete_users(usersToModify)
print(f'Successfully deleted {deletionResult.success_count} users')
print(f'Failed to delete {deletionResult.failure_count} users')
for error in deletionResult.errors:
    print(f'Error: {deletionResult.index}: {deletionResult.reason}')

# Clear Realtime Database page's 'students' field
ref.delete()