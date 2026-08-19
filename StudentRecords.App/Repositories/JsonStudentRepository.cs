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


        // ==============================
        // Get All Students
        // ==============================

        public List<Student> GetAll()
        {
            if (!File.Exists(_filePath))
            {
                return new List<Student>();
            }

            string json =
                File.ReadAllText(_filePath);

            if (string.IsNullOrWhiteSpace(json))
            {
                return new List<Student>();
            }

            return JsonSerializer.Deserialize<List<Student>>(json)
                   ?? new List<Student>();
        }


        // ==============================
        // Get Student By ID
        // ==============================

        public Student? GetById(int id)
        {
            List<Student> students =
                GetAll();

            return students.FirstOrDefault(
                student => student.Id == id);
        }


        // ==============================
        // Add Student
        // ==============================

        public void Add(Student student)
        {
            List<Student> students =
                GetAll();

            students.Add(student);

            SaveAll(students);
        }


        // ==============================
        // Update Student
        // ==============================

        public void Update(Student student)
        {
            List<Student> students =
                GetAll();

            Student? existingStudent =
                students.FirstOrDefault(
                    existing => existing.Id == student.Id);

            if (existingStudent != null)
            {
                existingStudent.Name =
                    student.Name;

                existingStudent.Surname =
                    student.Surname;

                existingStudent.Age =
                    student.Age;

                existingStudent.Course =
                    student.Course;

                existingStudent.Mark =
                    student.Mark;

                SaveAll(students);
            }
        }


        // ==============================
        // Delete Student
        // ==============================

        public void Delete(int id)
        {
            List<Student> students =
                GetAll();

            Student? studentToDelete =
                students.FirstOrDefault(
                    student => student.Id == id);

            if (studentToDelete != null)
            {
                students.Remove(studentToDelete);

                SaveAll(students);
            }
        }


        // ==============================
        // Save Students to JSON
        // ==============================

        private void SaveAll(List<Student> students)
        {
            string json =
                JsonSerializer.Serialize(
                    students,
                    new JsonSerializerOptions
                    {
                        WriteIndented = true
                    });

            File.WriteAllText(
                _filePath,
                json);
        }
    }
}