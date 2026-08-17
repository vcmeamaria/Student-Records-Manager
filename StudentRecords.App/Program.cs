using StudentRecords.App.Exceptions;
using StudentRecords.App.Models;
using StudentRecords.App.Repositories;
using StudentRecords.App.Services;

// Find the StudentRecords.App project folder.
string projectDirectory =
    Path.GetFullPath(
        Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));

// Create the path to the Data folder.
string dataDirectory =
    Path.Combine(projectDirectory, "Data");

string dataFilePath =
    Path.Combine(dataDirectory, "students.json");

// Create the Data folder if it does not exist.
Directory.CreateDirectory(dataDirectory);

// Create the repository.
IStudentRepository repository =
    new JsonStudentRepository(dataFilePath);

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
    Console.WriteLine("4. Update student");
    Console.WriteLine("5. Delete student");
    Console.WriteLine("6. Sort students");
    Console.WriteLine("7. Search students by name");
    Console.WriteLine("8. Exit");
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
                    Console.WriteLine($"Age: {student.Age}");
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
                Console.WriteLine($"Age: {student.Age}");
                Console.WriteLine($"Course: {student.Course}");
            }
            catch (StudentNotFoundException exception)
            {
                Console.WriteLine();
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

            Console.Write("Enter student age: ");
            string? ageInput = Console.ReadLine();

            if (!int.TryParse(ageInput, out int age))
            {
                Console.WriteLine("Invalid age. Please enter a number.");
                break;
            }

            Console.Write("Enter student course: ");
            string? course = Console.ReadLine();

            Student newStudent = new Student
            {
                Id = id,
                Name = name ?? "",
                Age = age,
                Course = course ?? ""
            };

            try
            {
                studentService.AddStudent(newStudent);

                Console.WriteLine();
                Console.WriteLine("Student added successfully.");
            }
            catch (ArgumentOutOfRangeException exception)
            {
                Console.WriteLine();
                Console.WriteLine(exception.Message);
            }
            catch (ArgumentException exception)
            {
                Console.WriteLine();
                Console.WriteLine(exception.Message);
            }

            break;

        case "4":
            Console.Write("Enter student ID to update: ");
            string? updateIdInput = Console.ReadLine();

            if (!int.TryParse(updateIdInput, out int updateId))
            {
                Console.WriteLine("Invalid ID. Please enter a number.");
                break;
            }

            Console.Write("Enter new student name: ");
            string? updatedName = Console.ReadLine();

            Console.Write("Enter new student age: ");
            string? updatedAgeInput = Console.ReadLine();

            if (!int.TryParse(updatedAgeInput, out int updatedAge))
            {
                Console.WriteLine("Invalid age. Please enter a number.");
                break;
            }

            Console.Write("Enter new student course: ");
            string? updatedCourse = Console.ReadLine();

            Student updatedStudent = new Student
            {
                Id = updateId,
                Name = updatedName ?? "",
                Age = updatedAge,
                Course = updatedCourse ?? ""
            };

            try
            {
                studentService.UpdateStudent(updatedStudent);

                Console.WriteLine();
                Console.WriteLine("Student updated successfully.");
            }
            catch (StudentNotFoundException exception)
            {
                Console.WriteLine();
                Console.WriteLine(exception.Message);
            }
            catch (ArgumentOutOfRangeException exception)
            {
                Console.WriteLine();
                Console.WriteLine(exception.Message);
            }
            catch (ArgumentException exception)
            {
                Console.WriteLine();
                Console.WriteLine(exception.Message);
            }

            break;

        case "5":
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
                Console.WriteLine();
                Console.WriteLine(exception.Message);
            }

            break;

        case "6":
            Console.WriteLine("Sort students by:");
            Console.WriteLine("1. Name");
            Console.WriteLine("2. Age");
            Console.WriteLine("3. Course");
            Console.WriteLine();

            Console.Write("Choose a sort option: ");
            string? sortChoice = Console.ReadLine();

            List<Student> sortedStudents;

            switch (sortChoice)
            {
                case "1":
                    sortedStudents =
                        studentService.GetStudentsSortedByName();
                    break;

                case "2":
                    sortedStudents =
                        studentService.GetStudentsSortedByAge();
                    break;

                case "3":
                    sortedStudents =
                        studentService.GetStudentsSortedByCourse();
                    break;

                default:
                    Console.WriteLine("Invalid sort option.");
                    break;
            }

            if (sortChoice == "1" ||
                sortChoice == "2" ||
                sortChoice == "3")
            {
                sortedStudents = sortChoice switch
                {
                    "1" => studentService.GetStudentsSortedByName(),
                    "2" => studentService.GetStudentsSortedByAge(),
                    "3" => studentService.GetStudentsSortedByCourse(),
                    _ => new List<Student>()
                };

                if (sortedStudents.Count == 0)
                {
                    Console.WriteLine("No students found.");
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Sorted Student Records:");
                    Console.WriteLine();

                    foreach (Student student in sortedStudents)
                    {
                        Console.WriteLine($"ID: {student.Id}");
                        Console.WriteLine($"Name: {student.Name}");
                        Console.WriteLine($"Age: {student.Age}");
                        Console.WriteLine($"Course: {student.Course}");
                        Console.WriteLine("----------------------");
                    }
                }
            }

            break;

        case "7":
            Console.Write("Enter part of the student name: ");
            string? searchName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(searchName))
            {
                Console.WriteLine("Please enter a name to search for.");
                break;
            }

            List<Student> matchingStudents =
                studentService.SearchStudentsByName(searchName);

            if (matchingStudents.Count == 0)
            {
                Console.WriteLine();
                Console.WriteLine("No matching students found.");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Matching Students:");
                Console.WriteLine();

                foreach (Student student in matchingStudents)
                {
                    Console.WriteLine($"ID: {student.Id}");
                    Console.WriteLine($"Name: {student.Name}");
                    Console.WriteLine($"Age: {student.Age}");
                    Console.WriteLine($"Course: {student.Course}");
                    Console.WriteLine("----------------------");
                }
            }

            break;

        case "8":
            keepRunning = false;
            Console.WriteLine("Goodbye!");
            break;

        default:
            Console.WriteLine("Invalid option. Please try again.");
            break;
    }
}