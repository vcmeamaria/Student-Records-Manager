namespace StudentRecords.App.Models
{
    public class Student : Person
    {
        public int Id { get; set; }

        public string Course { get; set; } = "";
    }
}