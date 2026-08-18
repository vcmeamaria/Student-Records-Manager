using StudentRecords.App.Exceptions;
using StudentRecords.App.Models;
using StudentRecords.App.Repositories;
using StudentRecords.App.Services;

var builder = WebApplication.CreateBuilder(args);

// Add API documentation.
builder.Services.AddOpenApi();

var app = builder.Build();

// Create the Data folder inside StudentRecords.App.
string solutionDirectory =
    Path.GetFullPath(
        Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."));

string dataDirectory =
    Path.Combine(
        solutionDirectory,
        "StudentRecords.App",
        "Data");

Directory.CreateDirectory(dataDirectory);

string dataFilePath =
    Path.Combine(dataDirectory, "students.json");

// Create the repository and service.
IStudentRepository repository =
    new JsonStudentRepository(dataFilePath);

StudentService studentService =
    new StudentService(repository);

// Enable OpenAPI documentation in development.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// GET: return all students.
app.MapGet("/students", () =>
{
    return Results.Ok(studentService.GetAllStudents());
});

// GET: return one student by ID.
app.MapGet("/students/{id:int}", (int id) =>
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

// POST: add a new student.
app.MapPost("/students", (Student student) =>
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

// PUT: update an existing student.
app.MapPut("/students/{id:int}", (int id, Student student) =>
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

// DELETE: delete a student.
app.MapDelete("/students/{id:int}", (int id) =>
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