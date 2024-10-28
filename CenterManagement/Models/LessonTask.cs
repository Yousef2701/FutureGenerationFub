namespace CenterManagement.Models
{
    public class LessonTask
    {
        public string Question { get; set; }

        public string LessonId { get; set; }

        public virtual Lesson Lesson { get; set; } = null!;
    }
}
