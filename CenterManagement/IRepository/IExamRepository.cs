using CenterManagement.Models;
using CenterManagement.ViewModels;

namespace CenterManagement.IRepository
{
    public interface IExamRepository
    {

        public Task<Exam> CreateNewExam(Exam model);

        public Task<Question> CreateNewQuestion(Question model);

        public Task<IEnumerable<Exam>> GetTeacherExamsList();

        public Task<IEnumerable<Question>> GetExamQuestionsList(string examId);

        public Task<int> GetExamQuestionsCount(string examId);

        public Task<IEnumerable<Result>> GetExamStudentsResultList(string examId);

        public Task<Question> GetQuestionData(Question model);

        public Task<Question> UpdateQuestionData(QuestionVM model);

        public Task<string> RemoveQuestion(Question model);

        public Task<string> DeleteExam(string examId);

    }
}
