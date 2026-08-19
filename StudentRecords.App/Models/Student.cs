namespace StudentRecords.App.Models
{
    public class Student : Person, IValidatable
    {
        public int Id { get; set; }

        public string Course { get; set; } = "";

        // Mark is optional.
        public int? Mark { get; set; }


        // ==============================
        // Generated Email
        // ==============================

        public string Email =>
            $"{CleanForEmail(Name)}{CleanForEmail(Surname)}{Id}@University.com";


        // ==============================
        // Generated Grade
        // ==============================

        public string Grade
        {
            get
            {
                if (!Mark.HasValue)
                {
                    return "/";
                }

                return GetGrade(Mark.Value);
            }
        }


        // ==============================
        // Convert Mark to Grade
        // ==============================

        public static string GetGrade(int mark)
        {
            if (mark >= 80)
            {
                return "A";
            }
            else if (mark >= 70)
            {
                return "B";
            }
            else if (mark >= 60)
            {
                return "C";
            }
            else if (mark >= 50)
            {
                return "D";
            }
            else
            {
                return "Fail";
            }
        }


        // ==============================
        // Clean Text for Email
        // ==============================

        private static string CleanForEmail(string value)
        {
            return new string(
                value
                    .Where(char.IsLetterOrDigit)
                    .ToArray());
        }


        // ==============================
        // Validate Text Field
        // ==============================

        private static void ValidateTextField(
            string value,
            string fieldName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(
                    $"{fieldName} is required.",
                    fieldName);
            }

            string trimmedValue =
                value.Trim();

            if (trimmedValue.Length < 3)
            {
                throw new ArgumentException(
                    $"{fieldName} must contain at least 3 characters.",
                    fieldName);
            }

            if (trimmedValue.Length > 25)
            {
                throw new ArgumentException(
                    $"{fieldName} cannot contain more than 25 characters.",
                    fieldName);
            }
        }


        // ==============================
        // Validation
        // ==============================

        public void Validate()
        {
            // ID must be positive.
            if (Id <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(Id),
                    "ID must be positive.");
            }


            // Name must be 3-25 characters.
            ValidateTextField(
                Name,
                nameof(Name));


            // Surname must be 3-25 characters.
            ValidateTextField(
                Surname,
                nameof(Surname));


            // Age must be between 16 and 80.
            if (Age < 16 || Age > 80)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(Age),
                    "Age must be between 16 and 80.");
            }


            // Course must be 3-25 characters.
            ValidateTextField(
                Course,
                nameof(Course));


            // Mark is optional.
            // If entered, it must be between 0 and 100.
            if (Mark.HasValue &&
                (Mark.Value < 0 || Mark.Value > 100))
            {
                throw new ArgumentOutOfRangeException(
                    nameof(Mark),
                    "Mark must be between 0 and 100.");
            }
        }
    }
}