namespace CenterManagement.Models
{
    public class Teacher
    {
        public string Id { get; set; }

        public string FristName { get; set; }

        public string LastName { get; set; }

        public string Subject { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string? ImageUrl { get; set; }

        public string? Governorate { get; set; } 

        public string? City { get; set; }

        public virtual ICollection<Lesson> Lessons { get; } = new List<Lesson>();

        public virtual ICollection<Barcode> Barcodes { get; } = new List<Barcode>();

        public virtual ICollection<Exam> Exams { get; } = new List<Exam>();
    }
}
