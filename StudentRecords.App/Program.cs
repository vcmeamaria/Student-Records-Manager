using StudentRecords.App.Repositories;
using StudentRecords.App.Services;

// Create the repository.
// Student data will be stored in students.json.
IStudentRepository repository =
    new JsonStudentRepository("students.json");

// Create the service.
StudentService studentService =
    new StudentService(repository);

// Keep the menu running.
bool keepRunning = true;

while (keepRunning)
{
    Console.WriteLine();
    Console.WriteLine("==============================");
    Console.WriteLine("   STUDENT RECORDS MANAGER");
    Console.WriteLine("==============================");
    Console.WriteLine();
    Console.WriteLine("1. View all students");
    Console.WriteLine("2. Find student by ID");
    Console.WriteLine("3. Add student");
    Console.WriteLine("4. Delete student");
    Console.WriteLine("5. Exit");
    Console.WriteLine();

    Console.Write("Choose an option: ");

    string? choice = Console.ReadLine();

    Console.WriteLine();

    switch (choice)
    {
        case "1":
            Console.WriteLine("View all students");
            break;

        case "2":
            Console.WriteLine("Find student by ID");
            break;

        case "3":
            Console.WriteLine("Add student");
            break;

        case "4":
            Console.WriteLine("Delete student");
            break;

        case "5":
            keepRunning = false;
            Console.WriteLine("Goodbye!");
            break;

        default:
            Console.WriteLine("Invalid option. Please try again.");
            break;
    }
}