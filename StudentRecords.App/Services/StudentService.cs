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

        public StudentService(
            IStudentRepository studentRepository,
            ILogger? logger = null)
        {
            _studentRepository = studentRepository;
            _logger = logger;
        }

        public List<Student> GetAllStudents()
        {
            return _studentRepository.GetAll();
        }

        public List<Student> GetStudentsSortedByName()
        {
            return _studentRepository
                .GetAll()
                .OrderBy(student => student.Name)
                .ToList();
        }

        public List<Student> GetStudentsSortedByAge()
        {
            return _studentRepository
                .GetAll()
                .OrderBy(student => student.Age)
                .ToList();
        }

        public List<Student> GetStudentsSortedByCourse()
        {
            return _studentRepository
                .GetAll()
                .OrderBy(student => student.Course)
                .ToList();
        }

        public List<Student> SearchStudentsByName(string searchTerm)
        {
            return _studentRepository
                .GetAll()
                .Where(student =>
                    student.Name.Contains(
                        searchTerm,
                        StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public Dictionary<string, int> GetCourseSummary()
        {
            return _studentRepository
                .GetAll()
                .GroupBy(student => student.Course)
                .ToDictionary(
                    group => group.Key,
                    group => group.Count());
        }

        public void ExportStudentsToCsv(string filePath)
        {
            List<Student> students = _studentRepository.GetAll();

            List<string> lines = new List<string>();

            lines.Add("Id,Name,Age,Course");

            foreach (Student student in students)
            {
                string line =
                    $"{student.Id},{student.Name},{student.Age},{student.Course}";

                lines.Add(line);
            }

            File.WriteAllLines(filePath, lines);

            _logger?.Log(
                $"Student records exported to CSV: {filePath}");
        }

        public Student GetStudentById(int id)
        {
            Student? student = _studentRepository.GetById(id);

            if (student == null)
            {
                throw new StudentNotFoundException(id);
            }

            return student;
        }

        public void AddStudent(Student student)
        {
            student.Validate();

            Student? existingStudent =
                _studentRepository.GetById(student.Id);

            if (existingStudent != null)
            {
                throw new ArgumentException(
                    $"A student with ID {student.Id} already exists.");
            }

            _studentRepository.Add(student);

            _logger?.Log(
                $"Student added: ID {student.Id} - {student.Name}");
        }

        public void UpdateStudent(Student student)
        {
            student.Validate();

            Student? existingStudent =
                _studentRepository.GetById(student.Id);

            if (existingStudent == null)
            {
                throw new StudentNotFoundException(student.Id);
            }

            _studentRepository.Update(student);

            _logger?.Log(
                $"Student updated: ID {student.Id} - {student.Name}");
        }

        public void DeleteStudent(int id)
        {
            Student? student = _studentRepository.GetById(id);

            if (student == null)
            {
                throw new StudentNotFoundException(id);
            }

            _studentRepository.Delete(id);

            _logger?.Log(
                $"Student deleted: ID {student.Id} - {student.Name}");
        }
    }
}