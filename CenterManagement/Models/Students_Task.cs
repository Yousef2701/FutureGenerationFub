namespace CenterManagement.Models
{
    public class Students_Task
    {
        public string FileUrl { get; set; }

        public string StudentId { get; set; }

        public string LessonId { get; set;}

        public virtual Student Student { get; set; } = null!;

        public virtual Lesson Lesson { get; set; } = null!;
    }
}
