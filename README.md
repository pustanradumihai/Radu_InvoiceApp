# Project Setup Instructions

Follow the steps below to set up and run the project on your local machine.

## Step 1: Clone the Repository

1. Open a terminal or Git client.
2. Navigate to the folder where you want to clone the project.
3. Run the following command:
   ```bash
   git clone <repository-url>
   ```
4. Navigate into the cloned repository:
   ```bash
   cd <repository-folder>
   ```

## Step 2: Create a Local Database

This project uses **mssqllocaldb** (which comes with Visual Studio) and **Entity Framework Core** for database management. To set up the local database:

1. In VS go to **Tools** -> **Add Database** -> **Change** Data source and select these options:
![image](https://github.com/user-attachments/assets/606d2a83-09a7-4e75-8807-681e9b5ae482)
2. Give your db a name and click OK.

![image](https://github.com/user-attachments/assets/ee1ddf88-81e6-4015-86c6-959980089ab6)

4. Update the connection string in `appsettings.json` if necessary:
   ```json
   "ConnectionStrings": {
       "DefaultConnection": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=**YOUR_DB_NAME**;Integrated Security=True;Encrypt=True"
   }
   ```

## Step 3: Apply Migrations

In order to turn our Models into DB Tables, we can use migrations.
To create the necessary database schema, you need to apply the migrations using the **Package Manager Console** in Visual Studio:

1. Go to **Tools** > **NuGet Package Manager** > **Package Manager Console**.
2. Run the following commands:
   ```bash
   Add-Migration InitialCreate
   ```
   This creates a migration script that represents the initial state of the database.


3. Apply the migration by running:
   ```bash
   Update-Database
   ```
   This creates the necessary tables and schema in the database.

## Step 4: Run the Project

1. Press **F5** or click on **Run** in Visual Studio to start the application.
2. The project will open in your default browser, and you can start interacting with the app.


By following these steps, you should be able to set up and run the project successfully on your local environment.

