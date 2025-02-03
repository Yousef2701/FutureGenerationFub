namespace CenterManagement.Models
{
    public class Result
    {
        public string Date { get; set; }

        public string Time { get; set; }

        public int CorrectAnswers { get; set; }

        public int WrongAnswers { get; set; }

        public string StudentId { get; set; }

        public string ExamId { get; set; }

        public virtual Student? Student { get; set; } 

        public virtual Exam? Exam { get; set; }
    }
}
