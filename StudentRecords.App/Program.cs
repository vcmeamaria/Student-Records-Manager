using StudentRecords.App.Exceptions;
using StudentRecords.App.Logging;
using StudentRecords.App.Models;
using StudentRecords.App.Repositories;
using StudentRecords.App.Services;


// ==============================
// Data Setup
// ==============================

string projectDirectory =
    Path.GetFullPath(
        Path.Combine(
            AppContext.BaseDirectory,
            "..",
            "..",
            ".."));

string dataDirectory =
    Path.Combine(
        projectDirectory,
        "Data");

string dataFilePath =
    Path.Combine(
        dataDirectory,
        "students.json");

Directory.CreateDirectory(dataDirectory);


// ==============================
// Repository
// ==============================

IStudentRepository repository =
    new JsonStudentRepository(
        dataFilePath);


// ==============================
// Logger
// ==============================

string logFilePath =
    Path.Combine(
        dataDirectory,
        "student-records.log");

ILogger logger =
    new FileLogger(
        logFilePath);


// ==============================
// Service
// ==============================

StudentService studentService =
    new StudentService(
        repository,
        logger);


// ==============================
// Main Menu
// ==============================

bool keepRunning = true;


while (keepRunning)
{
    Console.WriteLine();

    Console.WriteLine(
        "==============================");

    Console.WriteLine(
        "   STUDENT RECORDS MANAGER");

    Console.WriteLine(
        "==============================");

    Console.WriteLine();

    Console.WriteLine(
        "1. View all students");

    Console.WriteLine(
        "2. Find student by ID");

    Console.WriteLine(
        "3. Add student");

    Console.WriteLine(
        "4. Update student");

    Console.WriteLine(
        "5. Delete student");

    Console.WriteLine(
        "6. Sort students");

    Console.WriteLine(
        "7. Search students");

    Console.WriteLine(
        "8. Course summary");

    Console.WriteLine(
        "9. Export students to CSV");

    Console.WriteLine(
        "10. Student marks and grades");

    Console.WriteLine(
        "11. Exit");

    Console.WriteLine();

    Console.Write(
        "Choose an option: ");

    string? choice =
        Console.ReadLine();

    Console.WriteLine();


    switch (choice)
    {
        // ==============================
        // 1. View All Students
        // ==============================

        case "1":

            List<Student> students =
                studentService.GetAllStudents();


            if (students.Count == 0)
            {
                Console.WriteLine(
                    "No students found.");
            }
            else
            {
                Console.WriteLine(
                    "Student Records:");

                Console.WriteLine();


                foreach (Student student in students)
                {
                    DisplayStudent(student);
                }
            }

            break;


        // ==============================
        // 2. Find Student By ID
        // ==============================

        case "2":

            Console.Write(
                "Enter student ID: ");

            string? searchIdInput =
                Console.ReadLine();


            if (!int.TryParse(
                searchIdInput,
                out int searchId))
            {
                Console.WriteLine(
                    "Invalid ID. Please enter a number.");

                break;
            }


            try
            {
                Student student =
                    studentService
                        .GetStudentById(
                            searchId);

                Console.WriteLine();

                Console.WriteLine(
                    "Student found:");

                Console.WriteLine();

                DisplayStudent(student);
            }
            catch (
                StudentNotFoundException exception)
            {
                Console.WriteLine();

                Console.WriteLine(
                    exception.Message);
            }

            break;


        // ==============================
        // 3. Add Student
        // ==============================

        case "3":

            Console.Write(
                "Enter student ID: ");

            string? idInput =
                Console.ReadLine();


            if (!int.TryParse(
                idInput,
                out int id))
            {
                Console.WriteLine(
                    "Invalid ID. Please enter a number.");

                break;
            }


            Console.Write(
                "Enter student name: ");

            string? name =
                Console.ReadLine();


            Console.Write(
                "Enter student surname: ");

            string? surname =
                Console.ReadLine();


            Console.Write(
                "Enter student age: ");

            string? ageInput =
                Console.ReadLine();


            if (!int.TryParse(
                ageInput,
                out int age))
            {
                Console.WriteLine(
                    "Invalid age. Please enter a number.");

                break;
            }


            Console.Write(
                "Enter student course: ");

            string? course =
                Console.ReadLine();


            DisplayGradeGuide();


            Console.Write(
                "Enter student mark (0-100), " +
                "or press Enter if not graded: ");

            string? markInput =
                Console.ReadLine();


            int? mark = null;


            if (!string.IsNullOrWhiteSpace(
                markInput))
            {
                if (!int.TryParse(
                    markInput,
                    out int parsedMark))
                {
                    Console.WriteLine(
                        "Invalid mark. Please enter a number.");

                    break;
                }

                mark = parsedMark;
            }


            Student newStudent =
                new Student
                {
                    Id = id,
                    Name = name ?? "",
                    Surname = surname ?? "",
                    Age = age,
                    Course = course ?? "",
                    Mark = mark
                };


            try
            {
                studentService
                    .AddStudent(
                        newStudent);


                Console.WriteLine();

                Console.WriteLine(
                    "Student added successfully.");


                Console.WriteLine(
                    $"Generated email: {newStudent.Email}");


                Console.WriteLine(
                    $"Grade: {newStudent.Grade}");
            }
            catch (
                ArgumentOutOfRangeException exception)
            {
                Console.WriteLine();

                Console.WriteLine(
                    exception.Message);
            }
            catch (
                ArgumentException exception)
            {
                Console.WriteLine();

                Console.WriteLine(
                    exception.Message);
            }

            break;


        // ==============================
        // 4. Update Student
        // ==============================

        case "4":

            Console.Write(
                "Enter student ID to update: ");

            string? updateIdInput =
                Console.ReadLine();


            if (!int.TryParse(
                updateIdInput,
                out int updateId))
            {
                Console.WriteLine(
                    "Invalid ID. Please enter a number.");

                break;
            }


            Console.Write(
                "Enter new student name: ");

            string? updatedName =
                Console.ReadLine();


            Console.Write(
                "Enter new student surname: ");

            string? updatedSurname =
                Console.ReadLine();


            Console.Write(
                "Enter new student age: ");

            string? updatedAgeInput =
                Console.ReadLine();


            if (!int.TryParse(
                updatedAgeInput,
                out int updatedAge))
            {
                Console.WriteLine(
                    "Invalid age. Please enter a number.");

                break;
            }


            Console.Write(
                "Enter new student course: ");

            string? updatedCourse =
                Console.ReadLine();


            DisplayGradeGuide();


            Console.Write(
                "Enter new student mark (0-100), " +
                "or press Enter if not graded: ");

            string? updatedMarkInput =
                Console.ReadLine();


            int? updatedMark = null;


            if (!string.IsNullOrWhiteSpace(
                updatedMarkInput))
            {
                if (!int.TryParse(
                    updatedMarkInput,
                    out int parsedMark))
                {
                    Console.WriteLine(
                        "Invalid mark. Please enter a number.");

                    break;
                }

                updatedMark =
                    parsedMark;
            }


            Student updatedStudent =
                new Student
                {
                    Id = updateId,
                    Name = updatedName ?? "",
                    Surname = updatedSurname ?? "",
                    Age = updatedAge,
                    Course = updatedCourse ?? "",
                    Mark = updatedMark
                };


            try
            {
                studentService
                    .UpdateStudent(
                        updatedStudent);


                Console.WriteLine();

                Console.WriteLine(
                    "Student updated successfully.");


                Console.WriteLine(
                    $"Email: {updatedStudent.Email}");


                Console.WriteLine(
                    $"Grade: {updatedStudent.Grade}");
            }
            catch (
                StudentNotFoundException exception)
            {
                Console.WriteLine();

                Console.WriteLine(
                    exception.Message);
            }
            catch (
                ArgumentOutOfRangeException exception)
            {
                Console.WriteLine();

                Console.WriteLine(
                    exception.Message);
            }
            catch (
                ArgumentException exception)
            {
                Console.WriteLine();

                Console.WriteLine(
                    exception.Message);
            }

            break;


        // ==============================
        // 5. Delete Student
        // ==============================

        case "5":

            Console.Write(
                "Enter student ID to delete: ");

            string? deleteIdInput =
                Console.ReadLine();


            if (!int.TryParse(
                deleteIdInput,
                out int deleteId))
            {
                Console.WriteLine(
                    "Invalid ID. Please enter a number.");

                break;
            }


            try
            {
                studentService
                    .DeleteStudent(
                        deleteId);

                Console.WriteLine();

                Console.WriteLine(
                    "Student deleted successfully.");
            }
            catch (
                StudentNotFoundException exception)
            {
                Console.WriteLine();

                Console.WriteLine(
                    exception.Message);
            }

            break;


        // ==============================
        // 6. Sort Students
        // ==============================

        case "6":

            Console.WriteLine(
                "Sort students by:");

            Console.WriteLine(
                "1. Name");

            Console.WriteLine(
                "2. Age");

            Console.WriteLine(
                "3. Course");

            Console.WriteLine();

            Console.Write(
                "Choose a sort option: ");

            string? sortChoice =
                Console.ReadLine();


            List<Student> sortedStudents =
                sortChoice switch
                {
                    "1" =>
                        studentService
                            .GetStudentsSortedByName(),

                    "2" =>
                        studentService
                            .GetStudentsSortedByAge(),

                    "3" =>
                        studentService
                            .GetStudentsSortedByCourse(),

                    _ =>
                        new List<Student>()
                };


            if (sortChoice != "1" &&
                sortChoice != "2" &&
                sortChoice != "3")
            {
                Console.WriteLine(
                    "Invalid sort option.");

                break;
            }


            foreach (
                Student student
                in sortedStudents)
            {
                DisplayStudent(student);
            }

            break;


        // ==============================
        // 7. Search Students
        // ==============================

        case "7":

            Console.Write(
                "Enter student name, surname or email: ");

            string? searchName =
                Console.ReadLine();


            if (string.IsNullOrWhiteSpace(
                searchName))
            {
                Console.WriteLine(
                    "Please enter something to search for.");

                break;
            }


            List<Student> matchingStudents =
                studentService
                    .SearchStudentsByName(
                        searchName);


            if (matchingStudents.Count == 0)
            {
                Console.WriteLine(
                    "No matching students found.");
            }
            else
            {
                foreach (
                    Student student
                    in matchingStudents)
                {
                    DisplayStudent(student);
                }
            }

            break;


        // ==============================
        // 8. Course Summary
        // ==============================

        case "8":

            Dictionary<string, int> courseSummary =
                studentService
                    .GetCourseSummary();


            foreach (
                KeyValuePair<string, int>
                courseEntry
                in courseSummary)
            {
                Console.WriteLine(
                    $"{courseEntry.Key}: " +
                    $"{courseEntry.Value} student(s)");
            }

            break;


        // ==============================
        // 9. Export CSV
        // ==============================

        case "9":

            string csvFilePath =
                Path.Combine(
                    dataDirectory,
                    "students.csv");


            studentService
                .ExportStudentsToCsv(
                    csvFilePath);


            Console.WriteLine(
                "Students exported successfully.");


            Console.WriteLine(
                $"CSV file: {csvFilePath}");

            break;


        // ==============================
        // 10. Exercise 6
        // Student Marks Dictionary
        // ==============================

        case "10":

            Console.WriteLine(
                "==============================");

            Console.WriteLine(
                "   STUDENT MARKS AND GRADES");

            Console.WriteLine(
                "==============================");

            Console.WriteLine();


            DisplayGradeGuide();


            Dictionary<string, int> studentMarks =
                studentService
                    .GetStudentMarksDictionary();


            if (studentMarks.Count == 0)
            {
                Console.WriteLine(
                    "No graded students found.");

                break;
            }


            foreach (
                KeyValuePair<string, int>
                studentEntry
                in studentMarks)
            {
                string grade =
                    Student.GetGrade(
                        studentEntry.Value);


                Console.WriteLine(
                    $"Student: {studentEntry.Key}");


                Console.WriteLine(
                    $"Mark: {studentEntry.Value}");


                Console.WriteLine(
                    $"Grade: {grade}");


                Console.WriteLine(
                    "------------------------------");
            }

            break;


        // ==============================
        // 11. Exit
        // ==============================

        case "11":

            keepRunning = false;

            Console.WriteLine(
                "Goodbye!");

            break;


        default:

            Console.WriteLine(
                "Invalid option. Please try again.");

            break;
    }
}


// ==============================
// Display Student
// ==============================

static void DisplayStudent(
    Student student)
{
    string mark =
        student.Mark?.ToString() ?? "/";


    Console.WriteLine(
        $"ID: {student.Id}");

    Console.WriteLine(
        $"Name: {student.Name}");

    Console.WriteLine(
        $"Surname: {student.Surname}");

    Console.WriteLine(
        $"Email: {student.Email}");

    Console.WriteLine(
        $"Age: {student.Age}");

    Console.WriteLine(
        $"Course: {student.Course}");

    Console.WriteLine(
        $"Mark: {mark}");

    Console.WriteLine(
        $"Grade: {student.Grade}");

    Console.WriteLine(
        "------------------------------");
}


// ==============================
// Display Grade Guide
// ==============================

static void DisplayGradeGuide()
{
    Console.WriteLine(
        "Grade Guide:");

    Console.WriteLine(
        "A    = 80 - 100");

    Console.WriteLine(
        "B    = 70 - 79");

    Console.WriteLine(
        "C    = 60 - 69");

    Console.WriteLine(
        "D    = 50 - 59");

    Console.WriteLine(
        "Fail = 0 - 49");

    Console.WriteLine(
        "/    = Not graded");

    Console.WriteLine();
}