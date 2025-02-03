namespace CenterManagement.Models
{
    public class Question
    {
        public int Numbre { get; set; }

        public string Quest { get; set; }

        public string FristAnswer { get; set; }

        public string SecondAnswer { get; set; }

        public string ThirdAnswer { get; set; }

        public string ForthAnswer { get; set; }

        public string CorrectAnswer { get; set; }

        public string ExamId { get; set; }

        public virtual Exam? Exam { get; set; } 
    }
}
