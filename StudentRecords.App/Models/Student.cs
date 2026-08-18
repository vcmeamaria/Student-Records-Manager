namespace StudentRecords.App.Models
{
    public class Student : Person, IValidatable
    {
        public int Id { get; set; }

        public string Course { get; set; } = "";

        public void Validate()
        {
            if (Id <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(Id),
                    "ID must be positive.");
            }

            if (string.IsNullOrWhiteSpace(Name))
            {
                throw new ArgumentException(
                    "Name is required.",
                    nameof(Name));
            }

            if (Age < 16 || Age > 80)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(Age),
                    "Age must be between 16 and 80.");
            }

            if (string.IsNullOrWhiteSpace(Course))
            {
                throw new ArgumentException(
                    "Course is required.",
                    nameof(Course));
            }
        }
    }
}