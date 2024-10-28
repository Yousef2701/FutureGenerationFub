namespace CenterManagement.Models
{
    public class Exam
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string AcademyYear { get; set; }

        public int Minutes { get; set; }

        public string TeacherId { get; set; }

        public virtual Teacher Teacher { get; set; } = null!;

        public virtual ICollection<Question> Questions { get; } = new List<Question>();

        public virtual ICollection<Result> Results { get; } = new List<Result>();
    }
}
