using StudentRecords.App.Exceptions;
using StudentRecords.App.Models;
using StudentRecords.App.Services;

namespace StudentRecords.Tests
{
    public class StudentServiceTests
    {
        private readonly InMemoryStudentRepository _repository;
        private readonly StudentService _service;

        public StudentServiceTests()
        {
            _repository = new InMemoryStudentRepository();
            _service = new StudentService(_repository);
        }

        [Fact]
        public void AddStudent_AddsStudentSuccessfully()
        {
            Student student = new Student
            {
                Id = 1,
                Name = "Maria",
                Age = 22,
                Course = "Cyber Security"
            };

            _service.AddStudent(student);

            List<Student> students = _service.GetAllStudents();

            Assert.Single(students);
            Assert.Equal("Maria", students[0].Name);
        }

        [Fact]
        public void GetStudentById_ReturnsCorrectStudent()
        {
            Student student = new Student
            {
                Id = 1,
                Name = "Maria",
                Age = 22,
                Course = "Cyber Security"
            };

            _service.AddStudent(student);

            Student result = _service.GetStudentById(1);

            Assert.Equal(1, result.Id);
            Assert.Equal("Maria", result.Name);
            Assert.Equal(22, result.Age);
            Assert.Equal("Cyber Security", result.Course);
        }

        [Fact]
        public void GetStudentById_WhenStudentDoesNotExist_ThrowsException()
        {
            Assert.Throws<StudentNotFoundException>(
                () => _service.GetStudentById(99));
        }

        [Fact]
        public void UpdateStudent_UpdatesExistingStudent()
        {
            Student student = new Student
            {
                Id = 1,
                Name = "Maria",
                Age = 22,
                Course = "Cyber Security"
            };

            _service.AddStudent(student);

            Student updatedStudent = new Student
            {
                Id = 1,
                Name = "Maria Motter",
                Age = 23,
                Course = "Computer Science"
            };

            _service.UpdateStudent(updatedStudent);

            Student result = _service.GetStudentById(1);

            Assert.Equal("Maria Motter", result.Name);
            Assert.Equal(23, result.Age);
            Assert.Equal("Computer Science", result.Course);
        }

        [Fact]
        public void UpdateStudent_WhenStudentDoesNotExist_ThrowsException()
        {
            Student student = new Student
            {
                Id = 99,
                Name = "Maria",
                Age = 22,
                Course = "Cyber Security"
            };

            Assert.Throws<StudentNotFoundException>(
                () => _service.UpdateStudent(student));
        }

        [Fact]
        public void DeleteStudent_RemovesStudent()
        {
            Student student = new Student
            {
                Id = 1,
                Name = "Maria",
                Age = 22,
                Course = "Cyber Security"
            };

            _service.AddStudent(student);

            _service.DeleteStudent(1);

            List<Student> students = _service.GetAllStudents();

            Assert.Empty(students);
        }

        [Fact]
        public void DeleteStudent_WhenStudentDoesNotExist_ThrowsException()
        {
            Assert.Throws<StudentNotFoundException>(
                () => _service.DeleteStudent(99));
        }

        [Theory]
        [InlineData(15)]
        [InlineData(81)]
        public void AddStudent_WithInvalidAge_ThrowsException(int age)
        {
            Student student = new Student
            {
                Id = 1,
                Name = "Maria",
                Age = age,
                Course = "Cyber Security"
            };

            Assert.Throws<ArgumentOutOfRangeException>(
                () => _service.AddStudent(student));
        }

        [Fact]
        public void AddStudent_WithInvalidId_ThrowsException()
        {
            Student student = new Student
            {
                Id = 0,
                Name = "Maria",
                Age = 22,
                Course = "Cyber Security"
            };

            Assert.Throws<ArgumentOutOfRangeException>(
                () => _service.AddStudent(student));
        }

        [Fact]
        public void AddStudent_WithEmptyName_ThrowsException()
        {
            Student student = new Student
            {
                Id = 1,
                Name = "",
                Age = 22,
                Course = "Cyber Security"
            };

            Assert.Throws<ArgumentException>(
                () => _service.AddStudent(student));
        }

        [Fact]
        public void AddStudent_WithEmptyCourse_ThrowsException()
        {
            Student student = new Student
            {
                Id = 1,
                Name = "Maria",
                Age = 22,
                Course = ""
            };

            Assert.Throws<ArgumentException>(
                () => _service.AddStudent(student));
        }
    }
}