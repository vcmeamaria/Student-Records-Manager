using StudentRecords.App.Exceptions;
using StudentRecords.App.Models;
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
            List<Student> students = studentService.GetAllStudents();

            if (students.Count == 0)
            {
                Console.WriteLine("No students found.");
            }
            else
            {
                Console.WriteLine("Student Records:");
                Console.WriteLine();

                foreach (Student student in students)
                {
                    Console.WriteLine($"ID: {student.Id}");
                    Console.WriteLine($"Name: {student.Name}");
                    Console.WriteLine($"Course: {student.Course}");
                    Console.WriteLine("----------------------");
                }
            }

            break;

        case "2":
            Console.Write("Enter student ID: ");
            string? searchIdInput = Console.ReadLine();

            if (!int.TryParse(searchIdInput, out int searchId))
            {
                Console.WriteLine("Invalid ID. Please enter a number.");
                break;
            }

            try
            {
                Student student =
                    studentService.GetStudentById(searchId);

                Console.WriteLine();
                Console.WriteLine("Student found:");
                Console.WriteLine();

                Console.WriteLine($"ID: {student.Id}");
                Console.WriteLine($"Name: {student.Name}");
                Console.WriteLine($"Course: {student.Course}");
            }
            catch (StudentNotFoundException exception)
            {
                Console.WriteLine(exception.Message);
            }

            break;

        case "3":
            Console.Write("Enter student ID: ");
            string? idInput = Console.ReadLine();

            if (!int.TryParse(idInput, out int id))
            {
                Console.WriteLine("Invalid ID. Please enter a number.");
                break;
            }

            Console.Write("Enter student name: ");
            string? name = Console.ReadLine();

            Console.Write("Enter student course: ");
            string? course = Console.ReadLine();

            Student newStudent = new Student
            {
                Id = id,
                Name = name ?? "",
                Course = course ?? ""
            };

            studentService.AddStudent(newStudent);

            Console.WriteLine();
            Console.WriteLine("Student added successfully.");

            break;

        case "4":
            Console.Write("Enter student ID to delete: ");
            string? deleteIdInput = Console.ReadLine();

            if (!int.TryParse(deleteIdInput, out int deleteId))
            {
                Console.WriteLine("Invalid ID. Please enter a number.");
                break;
            }

            try
            {
                studentService.DeleteStudent(deleteId);

                Console.WriteLine();
                Console.WriteLine("Student deleted successfully.");
            }
            catch (StudentNotFoundException exception)
            {
                Console.WriteLine(exception.Message);
            }

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