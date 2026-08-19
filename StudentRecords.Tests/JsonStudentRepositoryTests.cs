using NUnit.Framework;
using StudentRecords.App.Models;
using StudentRecords.App.Repositories;

namespace StudentRecords.Tests
{
    [TestFixture]
    public class JsonStudentRepositoryTests
    {
        private string _testFilePath =
            null!;

        private JsonStudentRepository _repository =
            null!;


        [SetUp]
        public void SetUp()
        {
            _testFilePath =
                Path.Combine(
                    Path.GetTempPath(),
                    $"students-test-{Guid.NewGuid()}.json");

            _repository =
                new JsonStudentRepository(
                    _testFilePath);
        }


        [TearDown]
        public void TearDown()
        {
            if (File.Exists(
                _testFilePath))
            {
                File.Delete(
                    _testFilePath);
            }
        }


        [Test]
        public void GetAll_WhenFileDoesNotExist_ReturnsEmptyList()
        {
            List<Student> students =
                _repository.GetAll();

            Assert.That(
                students,
                Is.Empty);
        }


        [Test]
        public void Add_SavesStudentToFile()
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

            _repository.Add(student);


            List<Student> students =
                _repository.GetAll();


            Assert.That(
                students,
                Has.Count.EqualTo(1));

            Assert.That(
                students[0].Id,
                Is.EqualTo(1));

            Assert.That(
                students[0].Name,
                Is.EqualTo("Maria"));

            Assert.That(
                students[0].Surname,
                Is.EqualTo("Motter"));

            Assert.That(
                students[0].Age,
                Is.EqualTo(22));

            Assert.That(
                students[0].Course,
                Is.EqualTo("Cyber Security"));

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


        [Test]
        public void GetById_WhenStudentExists_ReturnsStudent()
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

            _repository.Add(student);


            Student? result =
                _repository.GetById(1);


            Assert.That(
                result,
                Is.Not.Null);

            Assert.That(
                result!.Name,
                Is.EqualTo("Maria"));

            Assert.That(
                result.Surname,
                Is.EqualTo("Motter"));

            Assert.That(
                result.Mark,
                Is.EqualTo(85));
        }


        [Test]
        public void GetById_WhenStudentDoesNotExist_ReturnsNull()
        {
            Student? result =
                _repository.GetById(99);

            Assert.That(
                result,
                Is.Null);
        }


        [Test]
        public void Update_ChangesExistingStudent()
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

            _repository.Add(student);


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


            _repository.Update(
                updatedStudent);


            Student? result =
                _repository.GetById(1);


            Assert.That(
                result,
                Is.Not.Null);

            Assert.That(
                result!.Name,
                Is.EqualTo("Maria"));

            Assert.That(
                result.Surname,
                Is.EqualTo("Smith"));

            Assert.That(
                result.Age,
                Is.EqualTo(23));

            Assert.That(
                result.Course,
                Is.EqualTo(
                    "Computer Science"));

            Assert.That(
                result.Mark,
                Is.EqualTo(85));

            Assert.That(
                result.Email,
                Is.EqualTo(
                    "MariaSmith1@University.com"));

            Assert.That(
                result.Grade,
                Is.EqualTo("A"));
        }


        [Test]
        public void Delete_RemovesStudent()
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

            _repository.Add(student);

            _repository.Delete(1);


            List<Student> students =
                _repository.GetAll();


            Assert.That(
                students,
                Is.Empty);
        }
    }
}