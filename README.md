# Student Management Console Application
A C# console application for performing CRUD (Create, Read, Update, Delete) operations on student records using ADO.NET with a shared service library.

## Features
* Read Students: View all active student records
* Create Student: Add new student records
* Update Student: Modify existing student information
* Delete Student: Soft delete students (sets DeleteFlag to 1)

## Project Structure
The solution consists of two main components:
### 1. ADONet.Shared Library
* Contains the core database service class AdoDotNetService
* Provides database connection management and query execution
* Uses parameterized queries for security

### 2. ADONet_ShareService Console Application
* User-friendly console interface
* Menu-driven CRUD operations
* Input validation and error handling

## Database Schema
The application uses the following table structure:

        sql
        CREATE TABLE Tbl_Student (
            Id INT PRIMARY KEY IDENTITY,
            RollNo NVARCHAR(50),
            Name NVARCHAR(100),
            Email NVARCHAR(100),
            DeleteFlag BIT DEFAULT 0
        );

## Prerequisites
* .NET Framework
* SQL Server
* Database named "StudentDB" with the above table structure
* Appropriate connection string configuration

## Installation
1. Clone the repository
2. Restore NuGet packages
3. Update the connection string in Program.cs:
        csharp
        private static readonly string _connectionString = "Your_Connection_String_Here";
4. Ensure the database and table are created

## Usage
Run the application and follow the menu prompts:

        text
        Menu - R : Read Student, C : Create Student, U : Update Student, D : Delete Student : 

## Available Operations
* R: Read/View all active students
* C: Create a new student record
* U: Update an existing student record
* D: Delete a student record (soft delete)
* Any other key: Exit the application

## Code Overview
### Shared Service Library
AdoDotNetService.cs
* Handles database connections and query execution
* Uses SqlDataAdapter to fill DataTable objects
* Supports parameterized queries for security

SqlParameterModel.cs
* Simple model class for SQL parameters

### Console Application
Program.cs
* Main entry point with menu system
* Contains methods for each CRUD operation
* Input validation and user interaction

## Key Features
* Soft Delete: Records are marked as deleted instead of being physically removed
* Parameterized Queries: Prevents SQL injection attacks
* Input Validation: Checks for valid student IDs and data
* Error Handling: Basic error handling for database operations

## Security Notes
* The application uses parameterized queries to prevent SQL injection
* Connection strings should be stored securely in production
* Consider using dependency injection for better testability

## Extension Possibilities
* Add search functionality
* Implement pagination for large datasets
* Add logging and more robust error handling
* Create a proper data access layer with repositories
* Add unit tests

## Contributing
Feel free to extend this application with additional features or improvements to the architecture.
