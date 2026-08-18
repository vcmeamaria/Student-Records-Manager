# Student Records Manager

A C# Student Records Manager created as part of my software development training.

The project started as a console application and was later extended with JSON persistence, validation, LINQ, automated testing with NUnit, logging, CSV export, an ASP.NET Core REST API, and a Razor Pages web interface.

## Features

### Console Application

- Add students
- View all students
- Find students by ID
- Update students
- Delete students
- Sort students by name, age, or course
- Search students by partial name
- View a course summary
- Export student records to CSV

### Web Interface

- Add new students
- View all student records
- Edit existing students
- Delete students
- Search students by name
- Filter students by course
- Sort students by ID, name, age, or course
- Display success and error messages

### Data and Validation

- Store student records in JSON
- Validate student information
- Prevent duplicate student IDs
- Log add, update, delete, and export actions
- Use a custom `StudentNotFoundException`

### REST API

The ASP.NET Core API supports:

```text
GET     /students
GET     /students/{id}
POST    /students
PUT     /students/{id}
DELETE  /students/{id}
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
│   └── Program.cs
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
7. Search students by name
8. Course summary
9. Export students to CSV
10. Exit
```

## Web Interface

The Razor Pages web interface provides a visual way to manage the same student records used by the console application.

The webpage uses C# Razor Pages for the application logic and HTML/CSS for the user interface.

Student data is handled through the existing `StudentService` and `JsonStudentRepository`.

## Project Architecture

```text
Student
   ↓
StudentService
   ↓
IStudentRepository
   ↓
JsonStudentRepository
   ↓
students.json
```

The same service layer can be accessed through:

```text
Console Application
Web Interface
REST API
```

## Testing

The project uses NUnit for automated testing.

Tests cover:

- Adding students
- Updating students
- Deleting students
- Finding students
- Invalid student data
- Duplicate student IDs
- Missing students
- JSON repository operations

Run the tests with:

```bash
dotnet test
```

## Run the Console Application

```bash
dotnet run --project StudentRecords.App
```

## Run the Web Application and API

```bash
dotnet run --project StudentRecords.Api
```

Then open the localhost address shown in the terminal.

For example:

```text
http://localhost:5073
```

## What I Practised

- C# syntax
- Object-Oriented Programming
- Classes and objects
- Inheritance
- Interfaces
- Encapsulation
- Polymorphism
- Repository pattern
- Service layer architecture
- Dependency injection
- Exception handling
- Validation
- LINQ
- JSON persistence
- File handling
- CSV export
- Logging
- NUnit automated testing
- ASP.NET Core
- REST APIs
- Razor Pages
- HTML and CSS
- CRUD operations
- Git branches
- GitHub pull requests
