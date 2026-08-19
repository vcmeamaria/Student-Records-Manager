# Student Records Manager

A C# Student Records Manager created as part of my software development training.

The project started as a console application and was later extended with JSON persistence, validation, LINQ, NUnit testing, logging, CSV export, an ASP.NET Core REST API, and a Razor Pages web interface.

## Features

### Console Application

- Add students
- View all students
- Find students by ID
- Update students
- Delete students
- Sort students by name, age, or course
- Search students by name, surname, or email
- View a course summary
- Export student records to CSV
- View student marks and automatically generated grades
- Display a grade guide

### Student Information

Each student record contains:

- Student ID
- Name
- Surname
- Age
- Course
- Generated university email
- Optional mark
- Automatically generated grade

### Generated Email

A unique university email address is automatically created using:

```text
Name + Surname + Student ID + @University.com
```

Example:

```text
Name: Maria
Surname: Motter
ID: 1

MariaMotter1@University.com
```

Because every student ID must be unique, each generated email address is also unique.

### Validation

Student records are validated before they are added or updated.

Rules include:

- Student ID must be positive
- Student ID must be unique
- Name is required
- Name must contain between 3 and 25 characters
- Surname is required
- Surname must contain between 3 and 25 characters
- Course is required
- Course must contain between 3 and 25 characters
- Age must be between 16 and 80
- Mark is optional
- If a mark is entered, it must be between 0 and 100

## Student Marks and Grades

The project includes the Student Marks Dictionary exercise using:

```csharp
Dictionary<string, int>
```

The dictionary stores a student and their mark and is used to display marks and grades.

Only students who have been graded are included in the marks dictionary.

### Grade Guide

```text
A     = 80 - 100
B     = 70 - 79
C     = 60 - 69
D     = 50 - 59
Fail  = 0 - 49
/     = Not graded
```

If a student has not yet received a mark, both the mark and grade are displayed as:

```text
/
```

## Web Interface

The ASP.NET Core Razor Pages interface provides a visual way to manage student records.

The webpage supports:

- Add students
- View all student records
- Edit students
- Delete students
- Search by name, surname, or email
- Filter by course
- Sort by student ID
- Sort by name
- Sort by surname
- Sort by age
- Sort by course
- Sort by mark
- View marks and grades
- View the grade guide
- Display generated university emails
- Display success and validation messages

The grade guide uses:

- Pastel green for Grade A
- Neutral colours for Grades B, C, and D
- Pastel pink for Fail

The web interface uses the same `StudentService` and repository as the rest of the application.

## REST API

The ASP.NET Core REST API supports CRUD operations.

```text
GET     /students
GET     /students/{id}
POST    /students
PUT     /students/{id}
DELETE  /students/{id}
```

### CRUD

```text
Create  -> POST
Read    -> GET
Update  -> PUT
Delete  -> DELETE
```

## JSON Persistence

Student records are stored in:

```text
StudentRecords.App/Data/students.json
```

JSON allows student records to remain saved after the application closes.

### Serialization

```text
C# objects -> JSON
```

### Deserialization

```text
JSON -> C# objects
```

## LINQ

LINQ is used throughout the project for searching, sorting, filtering, and grouping student records.

Examples include:

```text
Where()          -> Filter records
OrderBy()        -> Sort records
FirstOrDefault() -> Find the first matching record
GroupBy()        -> Group records
Select()         -> Select values
Distinct()       -> Remove duplicates
ToList()         -> Convert results to a List
```

## Logging

The project includes file logging.

Actions such as adding, updating, deleting, and exporting student records can be recorded in:

```text
student-records.log
```

## CSV Export

Student information can be exported to:

```text
students.csv
```

The CSV contains:

```text
ID
Name
Surname
Email
Age
Course
Mark
Grade
```

## NUnit Testing

The project uses NUnit for automated testing.

Tests cover areas such as:

- Adding students
- Finding students
- Updating students
- Deleting students
- Missing students
- Duplicate student IDs
- Age validation
- Name validation
- Surname validation
- Course validation
- Mark validation
- Grade conversion
- Generated email addresses
- Student marks dictionary
- JSON repository operations

Important NUnit attributes used include:

```text
[TestFixture]
[SetUp]
[Test]
[TestCase]
[TearDown]
```

Tests generally follow:

```text
Arrange
Act
Assert
```

Run all tests with:

```bash
dotnet test
```

## Technologies

- C#
- .NET
- ASP.NET Core
- Razor Pages
- NUnit
- LINQ
- JSON
- CSV
- HTML
- CSS
- Git
- GitHub
- Visual Studio

## Project Structure

```text
StudentRecordsSolution/
│
├── StudentRecords.App/
│   ├── Data/
│   ├── Exceptions/
│   ├── Logging/
│   ├── Models/
│   ├── Repositories/
│   ├── Services/
│   └── Program.cs
│
├── StudentRecords.Tests/
│   ├── InMemoryStudentRepository.cs
│   ├── JsonStudentRepositoryTests.cs
│   └── StudentServiceTests.cs
│
├── StudentRecords.Api/
│   ├── Pages/
│   │   ├── Index.cshtml
│   │   ├── Index.cshtml.cs
│   │   └── _ViewImports.cshtml
│   │
│   ├── wwwroot/
│   │   └── css/
│   │       └── site.css
│   │
│   ├── Program.cs
│   └── StudentRecords.Api.http
│
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
7. Search students
8. Course summary
9. Export students to CSV
10. Student marks and grades
11. Exit
```

## Architecture

```text
Student
   |
   v
StudentService
   |
   v
IStudentRepository
   |
   v
JsonStudentRepository
   |
   v
students.json
```

The same application logic can be accessed through:

```text
Console Application
        |
        |
StudentService
        |
        +---- Razor Pages Web Interface
        |
        +---- REST API
```

### Main Responsibilities

```text
Model       -> Represents the data
Service     -> Business rules and application logic
Repository  -> Stores and retrieves data
Console     -> Command-line interaction
Razor Pages -> Web interaction
REST API    -> HTTP interaction
NUnit       -> Automated testing
```

## Run the Console Application

```bash
dotnet run --project StudentRecords.App
```

## Run the Web Application and REST API

```bash
dotnet run --project StudentRecords.Api
```

Then open the localhost address displayed in the terminal.

For example:

```text
http://localhost:5073
```

## Run the Tests

```bash
dotnet test
```

## What I Practised

- C# syntax
- Classes and objects
- Properties
- Methods
- Constructors
- Object-Oriented Programming
- Encapsulation
- Abstraction
- Inheritance
- Polymorphism
- Interfaces
- Nullable types
- Collections
- Dictionaries
- LINQ
- Lambda expressions
- Repository pattern
- Service layer architecture
- Dependency injection
- Validation
- Exception handling
- Custom exceptions
- JSON serialization and deserialization
- File handling
- CSV export
- Logging
- NUnit automated testing
- Arrange-Act-Assert
- ASP.NET Core
- REST APIs
- CRUD operations
- Razor Pages
- HTML
- CSS
- Git branches
- GitHub pull requests
