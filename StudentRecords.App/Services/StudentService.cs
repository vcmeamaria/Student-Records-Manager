using StudentRecords.App.Exceptions;
using StudentRecords.App.Logging;
using StudentRecords.App.Models;
using StudentRecords.App.Repositories;

namespace StudentRecords.App.Services
{
    public class StudentService
    {
        private readonly IStudentRepository _studentRepository;

        private readonly ILogger? _logger;


        // ==============================
        // Constructor
        // ==============================

        public StudentService(
            IStudentRepository studentRepository,
            ILogger? logger = null)
        {
            _studentRepository =
                studentRepository;

            _logger =
                logger;
        }


        // ==============================
        // Get All Students
        // ==============================

        public List<Student> GetAllStudents()
        {
            return _studentRepository
                .GetAll();
        }


        // ==============================
        // Sort Students
        // ==============================

        public List<Student> GetStudentsSortedByName()
        {
            return _studentRepository
                .GetAll()
                .OrderBy(
                    student =>
                        student.Name)
                .ThenBy(
                    student =>
                        student.Surname)
                .ToList();
        }


        public List<Student> GetStudentsSortedByAge()
        {
            return _studentRepository
                .GetAll()
                .OrderBy(
                    student =>
                        student.Age)
                .ToList();
        }


        public List<Student> GetStudentsSortedByCourse()
        {
            return _studentRepository
                .GetAll()
                .OrderBy(
                    student =>
                        student.Course)
                .ToList();
        }


        // ==============================
        // Search Students
        // ==============================

        public List<Student> SearchStudentsByName(
            string searchTerm)
        {
            return _studentRepository
                .GetAll()
                .Where(student =>
                    student.Name.Contains(
                        searchTerm,
                        StringComparison.OrdinalIgnoreCase)

                    ||

                    student.Surname.Contains(
                        searchTerm,
                        StringComparison.OrdinalIgnoreCase)

                    ||

                    $"{student.Name} {student.Surname}"
                        .Contains(
                            searchTerm,
                            StringComparison.OrdinalIgnoreCase)

                    ||

                    student.Email.Contains(
                        searchTerm,
                        StringComparison.OrdinalIgnoreCase))
                .ToList();
        }


        // ==============================
        // Course Summary
        // ==============================

        public Dictionary<string, int>
            GetCourseSummary()
        {
            return _studentRepository
                .GetAll()
                .GroupBy(
                    student =>
                        student.Course)
                .ToDictionary(
                    group =>
                        group.Key,

                    group =>
                        group.Count());
        }


        // ==============================
        // Student Marks Dictionary
        // ==============================

        public Dictionary<string, int>
            GetStudentMarksDictionary()
        {
            return _studentRepository
                .GetAll()

                .Where(
                    student =>
                        student.Mark.HasValue)

                .ToDictionary(
                    student =>
                        $"{student.Name} " +
                        $"{student.Surname} - " +
                        $"{student.Email}",

                    student =>
                        student.Mark!.Value);
        }


        // ==============================
        // Export Students to CSV
        // ==============================

        public void ExportStudentsToCsv(
            string filePath)
        {
            List<Student> students =
                _studentRepository
                    .GetAll();


            List<string> lines =
                new List<string>();


            lines.Add(
                "Id,Name,Surname,Email,Age,Course,Mark,Grade");


            foreach (
                Student student
                in students)
            {
                string mark =
                    student.Mark
                        ?.ToString()
                    ?? "/";


                string line =
                    $"{student.Id}," +
                    $"{student.Name}," +
                    $"{student.Surname}," +
                    $"{student.Email}," +
                    $"{student.Age}," +
                    $"{student.Course}," +
                    $"{mark}," +
                    $"{student.Grade}";


                lines.Add(
                    line);
            }


            File.WriteAllLines(
                filePath,
                lines);


            _logger?.Log(
                $"Student records exported to CSV: {filePath}");
        }


        // ==============================
        // Get Student By ID
        // ==============================

        public Student GetStudentById(
            int id)
        {
            Student? student =
                _studentRepository
                    .GetById(id);


            if (student == null)
            {
                throw new StudentNotFoundException(
                    id);
            }


            return student;
        }


        // ==============================
        // Add Student
        // ==============================

        public void AddStudent(
            Student student)
        {
            student.Validate();


            Student? existingStudent =
                _studentRepository
                    .GetById(
                        student.Id);


            if (existingStudent != null)
            {
                throw new ArgumentException(
                    $"A student with ID {student.Id} already exists.");
            }


            _studentRepository
                .Add(student);


            string mark =
                student.Mark
                    ?.ToString()
                ?? "/";


            _logger?.Log(
                $"Student added: " +
                $"ID {student.Id} | " +
                $"{student.Name} {student.Surname} | " +
                $"Age: {student.Age} | " +
                $"Course: {student.Course} | " +
                $"Mark: {mark} | " +
                $"Grade: {student.Grade} | " +
                $"Email: {student.Email}");
        }


        // ==============================
        // Update Student
        // ==============================

        public void UpdateStudent(
            Student student)
        {
            student.Validate();


            Student? existingStudent =
                _studentRepository
                    .GetById(
                        student.Id);


            if (existingStudent == null)
            {
                throw new StudentNotFoundException(
                    student.Id);
            }


            // Save the old values before update.
            string oldName =
                existingStudent.Name;

            string oldSurname =
                existingStudent.Surname;

            int oldAge =
                existingStudent.Age;

            string oldCourse =
                existingStudent.Course;

            int? oldMark =
                existingStudent.Mark;

            string oldGrade =
                existingStudent.Grade;

            string oldEmail =
                existingStudent.Email;


            // Perform update.
            _studentRepository
                .Update(student);


            string oldMarkText =
                oldMark
                    ?.ToString()
                ?? "/";


            string newMarkText =
                student.Mark
                    ?.ToString()
                ?? "/";


            // Detailed audit log.
            _logger?.Log(
                $"Student updated: " +
                $"ID {student.Id} | " +

                $"Name: {oldName} -> {student.Name} | " +

                $"Surname: {oldSurname} -> {student.Surname} | " +

                $"Age: {oldAge} -> {student.Age} | " +

                $"Course: {oldCourse} -> {student.Course} | " +

                $"Mark: {oldMarkText} -> {newMarkText} | " +

                $"Grade: {oldGrade} -> {student.Grade} | " +

                $"Email: {oldEmail} -> {student.Email}");
        }


        // ==============================
        // Delete Student
        // ==============================

        public void DeleteStudent(
            int id)
        {
            Student? student =
                _studentRepository
                    .GetById(id);


            if (student == null)
            {
                throw new StudentNotFoundException(
                    id);
            }


            // Capture details before deleting.
            string name =
                student.Name;

            string surname =
                student.Surname;

            string course =
                student.Course;

            string email =
                student.Email;


            _studentRepository
                .Delete(id);


            _logger?.Log(
                $"Student deleted: " +
                $"ID {id} | " +
                $"{name} {surname} | " +
                $"Course: {course} | " +
                $"Email: {email}");
        }
    }
}