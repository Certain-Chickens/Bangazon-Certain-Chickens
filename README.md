# Bangazon-Certain-Chickens

Welcome to Bangazon!  To begin, please clone our repository into your code after cd to the right directory in which you would like to save this project
- ```git@github.com:Certain-Chickens/Bangazon-Certain-Chickens.git```
After you have clone our repository, please do a ```dotnet restore``` in your terminal. 
Please be patient, as this could take a minute if you have none of these dependencies already installed.
Next, you will need to open up your zshrc file.  To do this type ``` code ~/.zshrc ```
Now, we will add a line of code at the bottom of your zshrc file. Here is mine as an exmaple.
``` export BANGAZON_DB="/Users/KevinHaggerty/workspace/csharp/Bangazon-Certain-Chickens/BANGAZON_DB.db" ```
You will want to add your pathname to the export BANGAZON_DB variable where you want to save your database.  
My database is saved in the directory where our source code lives. 
If you go to your code editor and right click on the a file you can click copy file path and change the last file to "BANGAZON_DB.db" as I have below.
After this is done, please close your zshrc file and then run this in your teriminal: ``` source ~/.zshrc ``` .  This will update any changes.

