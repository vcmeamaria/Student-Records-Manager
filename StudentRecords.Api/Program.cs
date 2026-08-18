using StudentRecords.App.Exceptions;
using StudentRecords.App.Models;
using StudentRecords.App.Repositories;
using StudentRecords.App.Services;

var builder = WebApplication.CreateBuilder(args);

// Allow OpenAPI.
builder.Services.AddOpenApi();

// Allow Razor Pages.
builder.Services.AddRazorPages();


// ==============================
// Student Data
// ==============================

string solutionDirectory =
    Path.GetFullPath(
        Path.Combine(
            AppContext.BaseDirectory,
            "..",
            "..",
            "..",
            ".."));

string dataDirectory =
    Path.Combine(
        solutionDirectory,
        "StudentRecords.App",
        "Data");

Directory.CreateDirectory(dataDirectory);

string dataFilePath =
    Path.Combine(
        dataDirectory,
        "students.json");


// ==============================
// Dependency Injection
// ==============================

builder.Services.AddSingleton<IStudentRepository>(
    new JsonStudentRepository(dataFilePath));

builder.Services.AddSingleton<StudentService>(
    serviceProvider =>
        new StudentService(
            serviceProvider.GetRequiredService<IStudentRepository>()));


// Build application.
var app = builder.Build();


// ==============================
// Development Tools
// ==============================

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


// ==============================
// Web Page
// ==============================

// Allow CSS and other static files.
app.UseStaticFiles();

// Enable Razor Pages.
app.MapRazorPages();


// ==============================
// Web API Endpoints
// ==============================

// GET all students.
app.MapGet("/students", (StudentService studentService) =>
{
    return Results.Ok(
        studentService.GetAllStudents());
});


// GET student by ID.
app.MapGet("/students/{id:int}",
    (int id, StudentService studentService) =>
    {
        try
        {
            Student student =
                studentService.GetStudentById(id);

            return Results.Ok(student);
        }
        catch (StudentNotFoundException exception)
        {
            return Results.NotFound(
                new
                {
                    message = exception.Message
                });
        }
    });


// POST new student.
app.MapPost("/students",
    (Student student, StudentService studentService) =>
    {
        try
        {
            studentService.AddStudent(student);

            return Results.Created(
                $"/students/{student.Id}",
                student);
        }
        catch (ArgumentException exception)
        {
            return Results.BadRequest(
                new
                {
                    message = exception.Message
                });
        }
    });


// PUT update student.
app.MapPut("/students/{id:int}",
    (
        int id,
        Student student,
        StudentService studentService
    ) =>
    {
        if (id != student.Id)
        {
            return Results.BadRequest(
                new
                {
                    message =
                        "The ID in the URL must match the student ID."
                });
        }

        try
        {
            studentService.UpdateStudent(student);

            return Results.Ok(student);
        }
        catch (StudentNotFoundException exception)
        {
            return Results.NotFound(
                new
                {
                    message = exception.Message
                });
        }
        catch (ArgumentException exception)
        {
            return Results.BadRequest(
                new
                {
                    message = exception.Message
                });
        }
    });


// DELETE student.
app.MapDelete("/students/{id:int}",
    (int id, StudentService studentService) =>
    {
        try
        {
            studentService.DeleteStudent(id);

            return Results.NoContent();
        }
        catch (StudentNotFoundException exception)
        {
            return Results.NotFound(
                new
                {
                    message = exception.Message
                });
        }
    });


app.Run();