using NUnit.Framework;
using StudentRecords.App.Exceptions;
using StudentRecords.App.Models;
using StudentRecords.App.Services;

namespace StudentRecords.Tests
{
    [TestFixture]
    public class StudentServiceTests
    {
        private InMemoryStudentRepository _repository = null!;
        private StudentService _service = null!;

        [SetUp]
        public void SetUp()
        {
            _repository = new InMemoryStudentRepository();
            _service = new StudentService(_repository);
        }

        [Test]
        public void AddStudent_AddsStudentSuccessfully()
        {
            // Arrange
            Student student = new Student
            {
                Id = 1,
                Name = "Maria",
                Age = 22,
                Course = "Cyber Security"
            };

            // Act
            _service.AddStudent(student);

            List<Student> students = _service.GetAllStudents();

            // Assert
            Assert.That(students, Has.Count.EqualTo(1));
            Assert.That(students[0].Name, Is.EqualTo("Maria"));
        }

        [Test]
        public void AddStudent_WithDuplicateId_ThrowsException()
        {
            // Arrange
            Student firstStudent = new Student
            {
                Id = 1,
                Name = "Maria",
                Age = 22,
                Course = "Cyber Security"
            };

            Student secondStudent = new Student
            {
                Id = 1,
                Name = "Ana",
                Age = 20,
                Course = "Computer Science"
            };

            _service.AddStudent(firstStudent);

            // Act and Assert
            ArgumentException? exception =
                Assert.Throws<ArgumentException>(
                    () => _service.AddStudent(secondStudent));

            Assert.That(
                exception!.Message,
                Does.Contain("already exists"));
        }

        [Test]
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

            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.Name, Is.EqualTo("Maria"));
            Assert.That(result.Age, Is.EqualTo(22));
            Assert.That(result.Course, Is.EqualTo("Cyber Security"));
        }

        [Test]
        public void GetStudentById_WhenStudentDoesNotExist_ThrowsException()
        {
            Assert.Throws<StudentNotFoundException>(
                () => _service.GetStudentById(99));
        }

        [Test]
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

            Assert.That(result.Name, Is.EqualTo("Maria Motter"));
            Assert.That(result.Age, Is.EqualTo(23));
            Assert.That(result.Course, Is.EqualTo("Computer Science"));
        }

        [Test]
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

        [Test]
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

            Assert.That(students, Is.Empty);
        }

        [Test]
        public void DeleteStudent_WhenStudentDoesNotExist_ThrowsException()
        {
            Assert.Throws<StudentNotFoundException>(
                () => _service.DeleteStudent(99));
        }

        [TestCase(15)]
        [TestCase(81)]
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

        [Test]
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

        [Test]
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

        [Test]
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