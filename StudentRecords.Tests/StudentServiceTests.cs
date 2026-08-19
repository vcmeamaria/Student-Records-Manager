using NUnit.Framework;
using StudentRecords.App.Exceptions;
using StudentRecords.App.Models;
using StudentRecords.App.Services;

namespace StudentRecords.Tests
{
    [TestFixture]
    public class StudentServiceTests
    {
        private InMemoryStudentRepository _repository =
            null!;

        private StudentService _service =
            null!;


        [SetUp]
        public void SetUp()
        {
            _repository =
                new InMemoryStudentRepository();

            _service =
                new StudentService(
                    _repository);
        }


        // ==============================
        // Add Student
        // ==============================

        [Test]
        public void AddStudent_AddsStudentSuccessfully()
        {
            // Arrange
            Student student = new Student
            {
                Id = 1,
                Name = "Maria",
                Surname = "Motter",
                Age = 22,
                Course = "Cyber Security",
                Mark = 85
            };

            // Act
            _service.AddStudent(student);

            List<Student> students =
                _service.GetAllStudents();

            // Assert
            Assert.That(
                students,
                Has.Count.EqualTo(1));

            Assert.That(
                students[0].Name,
                Is.EqualTo("Maria"));

            Assert.That(
                students[0].Surname,
                Is.EqualTo("Motter"));

            Assert.That(
                students[0].Mark,
                Is.EqualTo(85));

            Assert.That(
                students[0].Email,
                Is.EqualTo(
                    "MariaMotter1@University.com"));

            Assert.That(
                students[0].Grade,
                Is.EqualTo("A"));
        }


        // ==============================
        // Duplicate ID
        // ==============================

        [Test]
        public void AddStudent_WithDuplicateId_ThrowsException()
        {
            Student firstStudent =
                new Student
                {
                    Id = 1,
                    Name = "Maria",
                    Surname = "Motter",
                    Age = 22,
                    Course = "Cyber Security",
                    Mark = 85
                };

            Student secondStudent =
                new Student
                {
                    Id = 1,
                    Name = "Ana",
                    Surname = "Silva",
                    Age = 20,
                    Course = "Computer Science",
                    Mark = 75
                };

            _service.AddStudent(
                firstStudent);

            ArgumentException? exception =
                Assert.Throws<ArgumentException>(
                    () =>
                        _service.AddStudent(
                            secondStudent));

            Assert.That(
                exception!.Message,
                Does.Contain("already exists"));
        }


        // ==============================
        // Get Student
        // ==============================

        [Test]
        public void GetStudentById_ReturnsCorrectStudent()
        {
            Student student =
                new Student
                {
                    Id = 1,
                    Name = "Maria",
                    Surname = "Motter",
                    Age = 22,
                    Course = "Cyber Security",
                    Mark = 85
                };

            _service.AddStudent(student);

            Student result =
                _service.GetStudentById(1);

            Assert.That(
                result.Id,
                Is.EqualTo(1));

            Assert.That(
                result.Name,
                Is.EqualTo("Maria"));

            Assert.That(
                result.Surname,
                Is.EqualTo("Motter"));

            Assert.That(
                result.Age,
                Is.EqualTo(22));

            Assert.That(
                result.Course,
                Is.EqualTo("Cyber Security"));

            Assert.That(
                result.Mark,
                Is.EqualTo(85));

            Assert.That(
                result.Email,
                Is.EqualTo(
                    "MariaMotter1@University.com"));
        }


        [Test]
        public void GetStudentById_WhenStudentDoesNotExist_ThrowsException()
        {
            Assert.Throws<StudentNotFoundException>(
                () =>
                    _service.GetStudentById(99));
        }


        // ==============================
        // Update Student
        // ==============================

        [Test]
        public void UpdateStudent_UpdatesExistingStudent()
        {
            Student student =
                new Student
                {
                    Id = 1,
                    Name = "Maria",
                    Surname = "Motter",
                    Age = 22,
                    Course = "Cyber Security",
                    Mark = 75
                };

            _service.AddStudent(student);


            Student updatedStudent =
                new Student
                {
                    Id = 1,
                    Name = "Maria",
                    Surname = "Smith",
                    Age = 23,
                    Course = "Computer Science",
                    Mark = 85
                };


            _service.UpdateStudent(
                updatedStudent);


            Student result =
                _service.GetStudentById(1);


            Assert.That(
                result.Name,
                Is.EqualTo("Maria"));

            Assert.That(
                result.Surname,
                Is.EqualTo("Smith"));

            Assert.That(
                result.Age,
                Is.EqualTo(23));

            Assert.That(
                result.Course,
                Is.EqualTo("Computer Science"));

            Assert.That(
                result.Mark,
                Is.EqualTo(85));

            Assert.That(
                result.Grade,
                Is.EqualTo("A"));

            Assert.That(
                result.Email,
                Is.EqualTo(
                    "MariaSmith1@University.com"));
        }


        [Test]
        public void UpdateStudent_WhenStudentDoesNotExist_ThrowsException()
        {
            Student student =
                new Student
                {
                    Id = 99,
                    Name = "Maria",
                    Surname = "Motter",
                    Age = 22,
                    Course = "Cyber Security",
                    Mark = 85
                };

            Assert.Throws<StudentNotFoundException>(
                () =>
                    _service.UpdateStudent(
                        student));
        }


        // ==============================
        // Delete Student
        // ==============================

        [Test]
        public void DeleteStudent_RemovesStudent()
        {
            Student student =
                new Student
                {
                    Id = 1,
                    Name = "Maria",
                    Surname = "Motter",
                    Age = 22,
                    Course = "Cyber Security",
                    Mark = 85
                };

            _service.AddStudent(student);

            _service.DeleteStudent(1);

            List<Student> students =
                _service.GetAllStudents();

            Assert.That(
                students,
                Is.Empty);
        }


        [Test]
        public void DeleteStudent_WhenStudentDoesNotExist_ThrowsException()
        {
            Assert.Throws<StudentNotFoundException>(
                () =>
                    _service.DeleteStudent(99));
        }


        // ==============================
        // Age Validation
        // ==============================

        [TestCase(15)]
        [TestCase(81)]
        public void AddStudent_WithInvalidAge_ThrowsException(
            int age)
        {
            Student student =
                new Student
                {
                    Id = 1,
                    Name = "Maria",
                    Surname = "Motter",
                    Age = age,
                    Course = "Cyber Security",
                    Mark = 85
                };

            Assert.Throws<ArgumentOutOfRangeException>(
                () =>
                    _service.AddStudent(
                        student));
        }


        // ==============================
        // ID Validation
        // ==============================

        [Test]
        public void AddStudent_WithInvalidId_ThrowsException()
        {
            Student student =
                new Student
                {
                    Id = 0,
                    Name = "Maria",
                    Surname = "Motter",
                    Age = 22,
                    Course = "Cyber Security",
                    Mark = 85
                };

            Assert.Throws<ArgumentOutOfRangeException>(
                () =>
                    _service.AddStudent(
                        student));
        }


        // ==============================
        // Name Validation
        // ==============================

        [Test]
        public void AddStudent_WithEmptyName_ThrowsException()
        {
            Student student =
                new Student
                {
                    Id = 1,
                    Name = "",
                    Surname = "Motter",
                    Age = 22,
                    Course = "Cyber Security",
                    Mark = 85
                };

            Assert.Throws<ArgumentException>(
                () =>
                    _service.AddStudent(
                        student));
        }


        // ==============================
        // Surname Validation
        // ==============================

        [Test]
        public void AddStudent_WithEmptySurname_ThrowsException()
        {
            Student student =
                new Student
                {
                    Id = 1,
                    Name = "Maria",
                    Surname = "",
                    Age = 22,
                    Course = "Cyber Security",
                    Mark = 85
                };

            Assert.Throws<ArgumentException>(
                () =>
                    _service.AddStudent(
                        student));
        }


        // ==============================
        // Course Validation
        // ==============================

        [Test]
        public void AddStudent_WithEmptyCourse_ThrowsException()
        {
            Student student =
                new Student
                {
                    Id = 1,
                    Name = "Maria",
                    Surname = "Motter",
                    Age = 22,
                    Course = "",
                    Mark = 85
                };

            Assert.Throws<ArgumentException>(
                () =>
                    _service.AddStudent(
                        student));
        }


        // ==============================
        // Mark Validation
        // ==============================

        [TestCase(-1)]
        [TestCase(101)]
        public void AddStudent_WithInvalidMark_ThrowsException(
            int mark)
        {
            Student student =
                new Student
                {
                    Id = 1,
                    Name = "Maria",
                    Surname = "Motter",
                    Age = 22,
                    Course = "Cyber Security",
                    Mark = mark
                };

            Assert.Throws<ArgumentOutOfRangeException>(
                () =>
                    _service.AddStudent(
                        student));
        }


        // ==============================
        // Grade Conversion
        // ==============================

        [TestCase(85, "A")]
        [TestCase(75, "B")]
        [TestCase(65, "C")]
        [TestCase(55, "D")]
        [TestCase(45, "Fail")]
        public void GetGrade_ReturnsCorrectGrade(
            int mark,
            string expectedGrade)
        {
            string grade =
                Student.GetGrade(mark);

            Assert.That(
                grade,
                Is.EqualTo(expectedGrade));
        }


        // ==============================
        // Exercise 6 Dictionary Test
        // ==============================

        [Test]
        public void GetStudentMarksDictionary_ReturnsStudentAndMark()
        {
            Student student =
                new Student
                {
                    Id = 1,
                    Name = "Maria",
                    Surname = "Motter",
                    Age = 22,
                    Course = "Cyber Security",
                    Mark = 85
                };

            _service.AddStudent(student);


            Dictionary<string, int> marks =
                _service
                    .GetStudentMarksDictionary();


            Assert.That(
                marks,
                Has.Count.EqualTo(1));

            Assert.That(
                marks.Values.First(),
                Is.EqualTo(85));

            Assert.That(
                marks.Keys.First(),
                Does.Contain("Maria Motter"));

            Assert.That(
                marks.Keys.First(),
                Does.Contain(
                    "MariaMotter1@University.com"));
        }
    }
}