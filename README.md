Getting Git/GitHub

a. Create a GitHub account preferably with your UT email.
b. Download Git.

Using Git/GitHub via terminal

1. Open up a Gitbash terminal where you wish to create a repository (directory) onto your computer.

2. Clone the 3dcapstone repository.
   git clone https://github.com/mitchellmarshe/3dcapstone

3. Updating the repository.
   git status # This shows you what files have changed
   git add "file name" # Add this file to your commit log
   git add -A # Add all files to your commit log
   git commit -m "update message" # Initialize your commit with a reason
   git push -u origin master # Send update as a user to the master branch via the origin remote
   git pull origin master # Recieve updates from the master branch via the origin remote
   
4. Creating a new branch for developement.
   git checkout -b "branch name"
   git add
   git commit
   git push
   ***follow terminal instructions for remote setup
   
5. Merging branches from the master.
   git merge "branch name"
   ***fix conflicts
   git add
   git commit
   git push

Using GitHub via website

You can select a branch and version of the repository.
You can make a "New pull request" to merge branches.
You can "Create new file".
You can "Upload files".
You can "Find file".
You can "Clone or download" the repository and its files.
You can navigate the repository as you would a directory on a computer.
Feel free to learn what each tab is about.
   