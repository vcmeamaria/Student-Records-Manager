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
            _studentRepository.Add(student);
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
    }
}