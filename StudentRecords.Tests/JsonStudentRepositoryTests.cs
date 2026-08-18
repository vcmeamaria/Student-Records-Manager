using NUnit.Framework;
using StudentRecords.App.Models;
using StudentRecords.App.Repositories;

namespace StudentRecords.Tests
{
    [TestFixture]
    public class JsonStudentRepositoryTests
    {
        private string _testFilePath = null!;
        private JsonStudentRepository _repository = null!;

        [SetUp]
        public void SetUp()
        {
            _testFilePath =
                Path.Combine(
                    Path.GetTempPath(),
                    $"students-test-{Guid.NewGuid()}.json");

            _repository =
                new JsonStudentRepository(_testFilePath);
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(_testFilePath))
            {
                File.Delete(_testFilePath);
            }
        }

        [Test]
        public void GetAll_WhenFileDoesNotExist_ReturnsEmptyList()
        {
            List<Student> students = _repository.GetAll();

            Assert.That(students, Is.Empty);
        }

        [Test]
        public void Add_SavesStudentToFile()
        {
            Student student = new Student
            {
                Id = 1,
                Name = "Maria",
                Age = 22,
                Course = "Cyber Security"
            };

            _repository.Add(student);

            List<Student> students = _repository.GetAll();

            Assert.That(students, Has.Count.EqualTo(1));
            Assert.That(students[0].Id, Is.EqualTo(1));
            Assert.That(students[0].Name, Is.EqualTo("Maria"));
            Assert.That(students[0].Age, Is.EqualTo(22));
            Assert.That(students[0].Course, Is.EqualTo("Cyber Security"));
        }

        [Test]
        public void GetById_WhenStudentExists_ReturnsStudent()
        {
            Student student = new Student
            {
                Id = 1,
                Name = "Maria",
                Age = 22,
                Course = "Cyber Security"
            };

            _repository.Add(student);

            Student? result = _repository.GetById(1);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Name, Is.EqualTo("Maria"));
        }

        [Test]
        public void GetById_WhenStudentDoesNotExist_ReturnsNull()
        {
            Student? result = _repository.GetById(99);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Update_ChangesExistingStudent()
        {
            Student student = new Student
            {
                Id = 1,
                Name = "Maria",
                Age = 22,
                Course = "Cyber Security"
            };

            _repository.Add(student);

            Student updatedStudent = new Student
            {
                Id = 1,
                Name = "Maria Motter",
                Age = 23,
                Course = "Computer Science"
            };

            _repository.Update(updatedStudent);

            Student? result = _repository.GetById(1);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Name, Is.EqualTo("Maria Motter"));
            Assert.That(result.Age, Is.EqualTo(23));
            Assert.That(result.Course, Is.EqualTo("Computer Science"));
        }

        [Test]
        public void Delete_RemovesStudent()
        {
            Student student = new Student
            {
                Id = 1,
                Name = "Maria",
                Age = 22,
                Course = "Cyber Security"
            };

            _repository.Add(student);

            _repository.Delete(1);

            List<Student> students = _repository.GetAll();

            Assert.That(students, Is.Empty);
        }
    }
}