# CourseSubmission DatabaseStorage - A Console App with Entity Framework Core and SQL Server

This is a console application built using C# that uses Entity Framework Core to connect to a SQL Server database. The application is a case management system, where it should be possible to enter error reports for different things and change the status of these cases. It allows you to perform various operations related to managing data in the database.

## Requirements
- .NET 7
- Visual Studio 2022 or later
- SQL Server
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Tools

## How to run the app
1. Clone the repository to your local machine
2. Open the solution file (.sln) in Visual Studio
3. Build the solution
4. Install these packages using the Package Manager Console in Visual Studio by running the following command:<br>Install-Package Microsoft.EntityFrameworkCore.SqlServer <br>Install-Package Microsoft.EntityFrameworkCore.Tools
5. Update the connection string in the `DataContext` class located in `Contexts/DataContext.cs` in the `OnConfiguring` method to point to the location of your SQL Server database file
6. Run the app in debug mode (press F5)

## Functionality
The console app allows you to perform the following operations:
- Create a new case
- View all cases in the database
- View a specific case by its ID
- Update the status of a case
- Add a comment to a case
- View all comments for a specific case

## Database structure
The app uses a code-first approach to create the database and tables. The database structure is normalized in 1-3NF, with four tables that have a relationship to each other.

## Usage
The app provides a simple command-line interface that allows you to perform various operations related to managing data in the database. Simply follow the on-screen prompts to execute the desired operation.
