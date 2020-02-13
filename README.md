# NPV Code Exercise
to keep things simple, I have utilised what is already included when creating a new Web Application (MVC) template in Visual Studio. Jquery for client side scripts, bootstrap for UI design.


## Setup

- this project is built using .NET Core 3.1 and SQL Server 2017
- to run the project: 
 - make sure that your machine has [.NET Core 3.1 SDK](https://dotnet.microsoft.com/download ".NET Core 3.1 SDK") installed
 - open the project in Visual Studio 2019
 - publish the project NPV-Exercise.Database, this would create the database tables needed for the project
 - change the AppDatabaseConnectionString value found  in the necessary appSettings.json file with the valid connection string (on local environment, make sure its appSettings.Development.json)
 - Build the npv-exercise project
