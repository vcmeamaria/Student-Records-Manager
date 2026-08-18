using StudentRecords.App.Models;
using StudentRecords.App.Repositories;

namespace StudentRecords.Tests
{
    public class JsonStudentRepositoryTests : IDisposable
    {
        private readonly string _testFilePath;
        private readonly JsonStudentRepository _repository;

        public JsonStudentRepositoryTests()
        {
            _testFilePath =
                Path.Combine(
                    Path.GetTempPath(),
                    $"students-test-{Guid.NewGuid()}.json");

            _repository =
                new JsonStudentRepository(_testFilePath);
        }

        [Fact]
        public void GetAll_WhenFileDoesNotExist_ReturnsEmptyList()
        {
            List<Student> students = _repository.GetAll();

            Assert.Empty(students);
        }

        [Fact]
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

            Assert.Single(students);
            Assert.Equal(1, students[0].Id);
            Assert.Equal("Maria", students[0].Name);
            Assert.Equal(22, students[0].Age);
            Assert.Equal("Cyber Security", students[0].Course);
        }

        [Fact]
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

            Assert.NotNull(result);
            Assert.Equal("Maria", result.Name);
        }

        [Fact]
        public void GetById_WhenStudentDoesNotExist_ReturnsNull()
        {
            Student? result = _repository.GetById(99);

            Assert.Null(result);
        }

        [Fact]
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

            Assert.NotNull(result);
            Assert.Equal("Maria Motter", result.Name);
            Assert.Equal(23, result.Age);
            Assert.Equal("Computer Science", result.Course);
        }

        [Fact]
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

            Assert.Empty(students);
        }

        public void Dispose()
        {
            if (File.Exists(_testFilePath))
            {
                File.Delete(_testFilePath);
            }
        }
    }
}