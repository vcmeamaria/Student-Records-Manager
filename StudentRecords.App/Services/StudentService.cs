using StudentRecords.App.Exceptions;
using StudentRecords.App.Models;
using StudentRecords.App.Repositories;

namespace StudentRecords.App.Services
{
    public class StudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
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
            Validate(
                student.Id,
                student.Name,
                student.Age,
                student.Course);

            _studentRepository.Add(student);
        }

        public void UpdateStudent(Student student)
        {
            Validate(
                student.Id,
                student.Name,
                student.Age,
                student.Course);

            Student? existingStudent =
                _studentRepository.GetById(student.Id);

            if (existingStudent == null)
            {
                throw new StudentNotFoundException(student.Id);
            }

            _studentRepository.Update(student);
        }

        public void DeleteStudent(int id)
        {
            Student? student = _studentRepository.GetById(id);

            if (student == null)
            {
                throw new StudentNotFoundException(id);
            }

            _studentRepository.Delete(id);
        }

        private static void Validate(
            int id,
            string name,
            int age,
            string course)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(id),
                    "ID must be positive.");
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(
                    "Name is required.",
                    nameof(name));
            }

            if (age < 16 || age > 80)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(age),
                    "Age must be between 16 and 80.");
            }

            if (string.IsNullOrWhiteSpace(course))
            {
                throw new ArgumentException(
                    "Course is required.",
                    nameof(course));
            }
        }
    }
}