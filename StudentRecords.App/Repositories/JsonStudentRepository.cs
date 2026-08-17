using System.Text.Json;
using StudentRecords.App.Models;

namespace StudentRecords.App.Repositories
{
    public class JsonStudentRepository : IStudentRepository
    {
        private readonly string _filePath;

        public JsonStudentRepository(string filePath)
        {
            _filePath = filePath;
        }

        public List<Student> GetAll()
        {
            if (!File.Exists(_filePath))
            {
                return new List<Student>();
            }

            string json = File.ReadAllText(_filePath);

            if (string.IsNullOrWhiteSpace(json))
            {
                return new List<Student>();
            }

            return JsonSerializer.Deserialize<List<Student>>(json)
                   ?? new List<Student>();
        }

        public Student? GetById(int id)
        {
            List<Student> students = GetAll();

            return students.FirstOrDefault(student => student.Id == id);
        }

        public void Add(Student student)
        {
            List<Student> students = GetAll();

            students.Add(student);

            SaveAll(students);
        }

        public void Delete(int id)
        {
            List<Student> students = GetAll();

            Student? studentToDelete =
                students.FirstOrDefault(student => student.Id == id);

            if (studentToDelete != null)
            {
                students.Remove(studentToDelete);

                SaveAll(students);
            }
        }

        private void SaveAll(List<Student> students)
        {
            string json = JsonSerializer.Serialize(
                students,
                new JsonSerializerOptions
                {
                    WriteIndented = true
                });

            File.WriteAllText(_filePath, json);
        }
    }
}