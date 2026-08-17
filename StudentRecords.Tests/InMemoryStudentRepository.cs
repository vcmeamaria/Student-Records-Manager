using StudentRecords.App.Models;
using StudentRecords.App.Repositories;

namespace StudentRecords.Tests
{
    public class InMemoryStudentRepository : IStudentRepository
    {
        private readonly List<Student> _students = new();

        public List<Student> GetAll()
        {
            return _students;
        }

        public Student? GetById(int id)
        {
            return _students.FirstOrDefault(
                student => student.Id == id);
        }

        public void Add(Student student)
        {
            _students.Add(student);
        }

        public void Update(Student student)
        {
            Student? existingStudent =
                _students.FirstOrDefault(
                    existing => existing.Id == student.Id);

            if (existingStudent != null)
            {
                existingStudent.Name = student.Name;
                existingStudent.Age = student.Age;
                existingStudent.Course = student.Course;
            }
        }

        public void Delete(int id)
        {
            Student? studentToDelete =
                _students.FirstOrDefault(
                    student => student.Id == id);

            if (studentToDelete != null)
            {
                _students.Remove(studentToDelete);
            }
        }
    }
}