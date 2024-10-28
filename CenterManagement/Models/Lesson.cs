using System.ComponentModel.DataAnnotations.Schema;

namespace CenterManagement.Models
{
    public class Lesson
    {
        
        public string Id { get; set; }

        public string LessonTitle { get; set; }

        public int Numbre { get; set; }

        public string Discreption { get; set; }

        public string VedioUrl { get; set; }

        public string Month { get; set; }

        public string AcademyYear { get; set; }

        public string ThumbnailUrl { get; set; }

        public string TeacherId { get; set; }

        public virtual Teacher Teacher { get; set; } = null!;

        public virtual ICollection<LessonTask> LessonTasks { get; } = new List<LessonTask>();

        public virtual ICollection<Students_Task> Students_Tasks { get; } = new List<Students_Task>();
    }
}
