# Sanatorium
Web Solution for Sanatorium Database
Welcome to the Sanatorium Database web solution! 
This README file will guide you step by step on how to download the project from the GitHub repository, 
populate the database, and understand the application usage recommendations.
## Installation

1. Clone the repository: `https://github.com/sofia297p/Sanatorium_BD.git`
2. Open folder with project
 
## Using
 1.In appsettings.json
in line 
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=sanatoriumdb;Username=your-username;Password=your-password;"
},
Replace your-username with your PostgreSQL username and your-password with your updated password.
2.Open Tools-> NuGetPackage Manager->Package Manager Console
3.Run the following command:update-database

## Database Structure:
The Sanatorium Database consists of several tables, and they need to be populated with initial data. Each table, such as inspectors and alcoholics, requires specific information.

Populate the tables:
Use the add operation to insert data into the tables. Make sure to provide the necessary data for each table:

## Learning the Application
Actions and Queries:
The Sanatorium Database web solution offers various actions and queries for exploration. Always use the inspector's or alcoholic's identifier instead of the general person identifier in your queries.

## Query Testing:
Feel free to experiment with different actions and queries suggested by the application.

##Enjoy using the Sanatorium Database web solution!