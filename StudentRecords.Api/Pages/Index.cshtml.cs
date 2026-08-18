using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentRecords.App.Exceptions;
using StudentRecords.App.Models;
using StudentRecords.App.Services;

namespace StudentRecords.Api.Pages
{
    public class IndexModel : PageModel
    {
        private readonly StudentService _studentService;


        // ==============================
        // Add Student Form
        // ==============================

        [BindProperty]
        public int Id { get; set; }

        [BindProperty]
        public string Name { get; set; } = "";

        [BindProperty]
        public int Age { get; set; }

        [BindProperty]
        public string Course { get; set; } = "";


        // ==============================
        // Edit Student Form
        // ==============================

        [BindProperty]
        public int EditId { get; set; }

        [BindProperty]
        public string EditName { get; set; } = "";

        [BindProperty]
        public int EditAge { get; set; }

        [BindProperty]
        public string EditCourse { get; set; } = "";


        // ==============================
        // Filters
        // ==============================

        [BindProperty(SupportsGet = true)]
        public string SearchName { get; set; } = "";

        [BindProperty(SupportsGet = true)]
        public string CourseFilter { get; set; } = "";

        [BindProperty(SupportsGet = true)]
        public string SortBy { get; set; } = "";


        // ==============================
        // Student Data
        // ==============================

        public List<Student> Students { get; set; }
            = new List<Student>();

        public List<string> Courses { get; set; }
            = new List<string>();


        // ==============================
        // Messages
        // ==============================

        public string Message { get; set; } = "";

        public string ErrorMessage { get; set; } = "";


        // ==============================
        // Constructor
        // ==============================

        public IndexModel(StudentService studentService)
        {
            _studentService = studentService;
        }


        // ==============================
        // Load Page
        // ==============================

        public void OnGet()
        {
            LoadStudents();
        }


        // ==============================
        // Load Student for Editing
        // ==============================

        public void OnGetEdit(int id)
        {
            try
            {
                Student student =
                    _studentService.GetStudentById(id);

                EditId = student.Id;
                EditName = student.Name;
                EditAge = student.Age;
                EditCourse = student.Course;
            }
            catch (StudentNotFoundException exception)
            {
                ErrorMessage = exception.Message;
            }

            LoadStudents();
        }


        // ==============================
        // Add Student
        // ==============================

        public void OnPostAdd()
        {
            try
            {
                Student student = new Student
                {
                    Id = Id,
                    Name = Name,
                    Age = Age,
                    Course = Course
                };

                _studentService.AddStudent(student);

                Message =
                    $"Student {student.Name} added successfully.";

                ClearAddForm();
            }
            catch (ArgumentException exception)
            {
                ErrorMessage = exception.Message;
            }

            LoadStudents();
        }


        // ==============================
        // Update Student
        // ==============================

        public void OnPostUpdate()
        {
            try
            {
                Student student = new Student
                {
                    Id = EditId,
                    Name = EditName,
                    Age = EditAge,
                    Course = EditCourse
                };

                _studentService.UpdateStudent(student);

                Message =
                    $"Student {student.Name} updated successfully.";

                ClearEditForm();
            }
            catch (StudentNotFoundException exception)
            {
                ErrorMessage = exception.Message;
            }
            catch (ArgumentException exception)
            {
                ErrorMessage = exception.Message;
            }

            LoadStudents();
        }


        // ==============================
        // Delete Student
        // ==============================

        public void OnPostDelete(int id)
        {
            try
            {
                _studentService.DeleteStudent(id);

                Message =
                    $"Student ID {id} deleted successfully.";
            }
            catch (StudentNotFoundException exception)
            {
                ErrorMessage = exception.Message;
            }

            LoadStudents();
        }


        // ==============================
        // Load and Filter Students
        // ==============================

        private void LoadStudents()
        {
            List<Student> allStudents =
                _studentService.GetAllStudents();


            // Create course dropdown options.
            Courses = allStudents
                .Select(student => student.Course)
                .Distinct()
                .OrderBy(course => course)
                .ToList();


            IEnumerable<Student> filteredStudents =
                allStudents;


            // Search by name.
            if (!string.IsNullOrWhiteSpace(SearchName))
            {
                filteredStudents =
                    filteredStudents.Where(student =>
                        student.Name.Contains(
                            SearchName,
                            StringComparison.OrdinalIgnoreCase));
            }


            // Filter by course.
            if (!string.IsNullOrWhiteSpace(CourseFilter))
            {
                filteredStudents =
                    filteredStudents.Where(student =>
                        student.Course.Equals(
                            CourseFilter,
                            StringComparison.OrdinalIgnoreCase));
            }


            // Sort students.
            filteredStudents = SortBy switch
            {
                "name" =>
                    filteredStudents.OrderBy(
                        student => student.Name),

                "age" =>
                    filteredStudents.OrderBy(
                        student => student.Age),

                "course" =>
                    filteredStudents.OrderBy(
                        student => student.Course),

                _ =>
                    filteredStudents.OrderBy(
                        student => student.Id)
            };


            Students =
                filteredStudents.ToList();
        }


        // ==============================
        // Clear Add Form
        // ==============================

        private void ClearAddForm()
        {
            Id = 0;
            Name = "";
            Age = 0;
            Course = "";
        }


        // ==============================
        // Clear Edit Form
        // ==============================

        private void ClearEditForm()
        {
            EditId = 0;
            EditName = "";
            EditAge = 0;
            EditCourse = "";
        }
    }
}