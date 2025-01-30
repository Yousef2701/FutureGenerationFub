using CenterManagement.Data;
using CenterManagement.IRepository;
using CenterManagement.Models;
using CenterManagement.ViewModels;
using System.Security.Claims;

namespace CenterManagement.Repository
{
    public class ExamRepository : IExamRepository
    {

        #region Dependancey injuction

        private readonly ApplicationDbContext _context;
        private readonly IUserRepository _userRepository;

        public ExamRepository(ApplicationDbContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        #endregion


        #region Create New Exam

        public async Task<Exam> CreateNewExam(Exam model)
        {
            if(model != null)
            {
                var userId = await _userRepository.GitLoggingUserId();

                var exam = new Exam
                {
                    Id = model.Id,
                    Title = model.Title,
                    Minutes = model.Minutes,
                    AcademyYear = model.AcademyYear,
                    TeacherId = userId
                };
                _context.Exams.Add(exam);
                _context.SaveChanges();

                return exam;
            }
            return null;
        }

        #endregion

        #region Create New Question

        public async Task<Question> CreateNewQuestion(Question model)
        {
            if(model != null)
            {
                int count = _context.Questions.Where(m => m.ExamId == model.ExamId).Count();

                var question = new Question
                {
                    ExamId = model.ExamId,
                    Numbre = count + 1,
                    Quest = model.Quest,
                    ForthAnswer = model.ForthAnswer,
                    SecondAnswer = model.SecondAnswer,
                    ThirdAnswer = model.ThirdAnswer,
                    FristAnswer = model.FristAnswer,
                    CorrectAnswer = model.CorrectAnswer
                };
                _context.Questions.Add(question);
                _context.SaveChanges();

                return question;
            }
            return null;
        }

        #endregion

        #region Get Teacher Exams List

        public async Task<IEnumerable<Exam>> GetTeacherExamsList()
        {
            var userId = await _userRepository.GitLoggingUserId();

            if(userId != null)
            {
                var exams = _context.Exams.Where(m => m.TeacherId == userId).ToList();
                return exams;
            }
            return null;   
        }

        #endregion

        #region Get Exam Questions List

        public async Task<IEnumerable<Question>> GetExamQuestionsList(string examId)
        {
            if(examId != null)
            {
                var questions = _context.Questions.Where(m => m.ExamId == examId).ToList();
                return questions;
            }
            return null;
        }

        #endregion

        #region Get Exam Questions Count

        public async Task<int> GetExamQuestionsCount(string examId)
        {
            if(examId != null)
            {
                int count = _context.Questions.Where(m => m.ExamId == examId).Count();
                return count;
            }
            return 0;
        }

        #endregion

        #region Get Exam Students Result List

        public async Task<IEnumerable<Result>> GetExamStudentsResultList(string examId)
        {
            if(examId != null)
            {
                var results = _context.Results.Where(m => m.ExamId == examId).ToList();
                return results;
            }
            return null;
        }

        #endregion

        #region Get Student Exams List

        public async Task<IEnumerable<Result>> GetStudentExamsList()
        {
            var userId = await _userRepository.GitLoggingUserId();
            if(userId != null)
            {
                var results = _context.Results.Where(m => m.StudentId == userId).ToList();
                if(results != null)
                    return results;
            }
            return null;
        }

        #endregion

        #region Get Question Data

        public async Task<Question> GetQuestionData(Question model)
        {
            if(model != null)
            {
                var question = _context.Questions.Where(m => m.ExamId == model.ExamId & m.Numbre == model.Numbre).FirstOrDefault();
                return question;
            }
            return null;
        }

        #endregion

        #region Update Question Data

        public async Task<Question> UpdateQuestionData(QuestionVM model)
        {
            if(model != null)
            {
                var question = _context.Questions.Where(m => m.ExamId == model.ExamId && m.Numbre == model.Numbre).FirstOrDefault();

                question.ExamId = model.ExamId;
                question.Quest = model.Quest;
                question.FristAnswer = model.FristAnswer;
                question.SecondAnswer = model.SecondAnswer;
                question.ThirdAnswer = model.ThirdAnswer;
                question.ForthAnswer = model.ForthAnswer;
                question.CorrectAnswer = model.CorrectAnswer;

                _context.Questions.Update(question);
                _context.SaveChanges();

                return question;
            }
            return null;
        }

        #endregion

        #region Remove Question

        public async Task<string> RemoveQuestion(Question model)
        {
            if(model != null)
            {
                var question = _context.Questions.Where(m => m.ExamId == model.ExamId && m.Numbre == model.Numbre).FirstOrDefault();
                if(question != null)
                {
                    _context.Questions.Remove(question);
                    _context.SaveChanges();

                    return "Success";
                }
            }
            return "Falled!";
        }

        #endregion

        #region Delete Exam

        public async Task<string> DeleteExam(string examId)
        {
            if(examId != null)
            {
                var questions = _context.Questions.Where(m => m.ExamId == examId).ToList();
                if(questions.Count > 0)
                {
                    foreach (Question item in questions)
                    {
                        _context.Questions.Remove(item);
                        _context.SaveChanges();
                    }
                }

                var exam = _context.Exams.Find(examId);

                _context.Exams.Remove(exam);
                _context.SaveChanges();

                return "Success";
            }
            return "Falled!";
        }

        #endregion

    }
}
