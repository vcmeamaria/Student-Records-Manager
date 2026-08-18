# Student Records Manager

A C# Student Records Manager created as part of my software development training.

The project started as a console application and was later extended with JSON persistence, validation, LINQ, automated testing, logging, CSV export, and an ASP.NET Core Web API.

## Features

* Add students
* View all students
* Find students by ID
* Update students
* Delete students
* Sort students by name, age, or course
* Search students by partial name
* View a course summary
* Export student records to CSV
* Store student records in JSON
* Log add, update, delete, and export actions
* Validate student data
* Access student records through a REST API

## Technologies

* C#
* .NET
* ASP.NET Core
* xUnit
* LINQ
* JSON
* CSV
* Git
* GitHub
* Visual Studio

## Project Structure

```text
StudentRecordsSolution/
├── StudentRecords.App/
├── StudentRecords.Tests/
├── StudentRecords.Api/
├── .gitignore
├── README.md
└── StudentRecordsSolution.slnx
```

## Console Menu

```text
1. View all students
2. Find student by ID
3. Add student
4. Update student
5. Delete student
6. Sort students
7. Search students by name
8. Course summary
9. Export students to CSV
10. Exit
```

## Web API

The ASP.NET Core API supports:

```text
GET     /students
GET     /students/{id}
POST    /students
PUT     /students/{id}
DELETE  /students/{id}
```

## Testing

The project includes automated tests for the service layer and JSON repository.

Run the tests with:

```bash
dotnet test
```

## Run the Console App

```bash
dotnet run --project StudentRecords.App
```

## Run the API

```bash
dotnet run --project StudentRecords.Api
```

## What I Practised

* Object-Oriented Programming
* Inheritance
* Interfaces
* Repository pattern
* Service layer architecture
* Exception handling
* Validation
* LINQ
* JSON persistence
* File handling
* CSV export
* Logging
* Automated testing
* REST APIs
* Git branches and pull requests
