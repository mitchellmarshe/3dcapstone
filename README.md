Getting Git/GitHub

a. Create a GitHub account preferably with your UT email.
<br/>
b. Download Git.

Using Git/GitHub via terminal

1. Open up a Gitbash terminal where you wish to create a repository (directory) onto your computer.

2. Clone the 3dcapstone repository.
   <br/>
   git clone https://github.com/mitchellmarshe/3dcapstone

3. Updating the repository.
   <br/>
   git status # This shows you what files have changed
   <br/>
   git add "file name" # Add this file to your commit log
   <br/>
   git add -A # Add all files to your commit log
   <br/>
   git commit -m "update message" # Initialize your commit with a reason
   <br/>
   git push -u origin master # Send update as a user to the master branch via the origin remote
   <br/>
   git pull origin master # Recieve updates from the master branch via the origin remote
   
4. Creating a new branch for developement.
   <br/>
   git checkout -b "branch name"
   <br/>
   git add
   <br/>
   git commit
   <br/>
   git push
   <br/>
   ***follow terminal instructions for remote setup
   
5. Merging branches from the master.
   <br/>
   git merge "branch name"
   <br/>
   ***fix conflicts
   <br/>
   git add
   <br/>
   git commit
   <br/>
   git push
   <br/>

Using GitHub via website

You can select a branch and version of the repository.
<br/>
You can make a "New pull request" to merge branches.
<br/>
You can "Create new file".
<br/>
You can "Upload files".
<br/>
You can "Find file".
<br/>
You can "Clone or download" the repository and its files.
<br/>
You can navigate the repository as you would a directory on a computer.
<br/>
Feel free to learn what each tab is about.
   