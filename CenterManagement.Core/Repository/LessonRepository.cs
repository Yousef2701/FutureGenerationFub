using ArabityAuth;
using System.Security.Claims;
using CenterManagement.IRepository;
using CenterManagement.Models;
using CenterManagement.ViewModels;
using CenterManagement.Data.Data;

namespace CenterManagement.Repository
{
    public class LessonRepository : ILessonRepository
    {

        #region Dependancey injuction

        private readonly ApplicationDbContext _context;
        private readonly IUserRepository _userRepository;

        public LessonRepository(ApplicationDbContext context,
                                IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        #endregion


        #region Add New Lesson

        public async Task<Lesson> AddNewLesson(LessonVM model)
        {
            if(model != null)
            {
                var userId = await _userRepository.GitLoggingUserId();
                var user = _context.Users.Where(m => m.Id == userId).Select(m => m.UserName).FirstOrDefault();

                var vedio = new Tools();
                string vedioUrl = vedio.AddVedios(model.VedioFile, user);

                var image = new Tools();
                string imageUrl = image.AddImages(model.ImageFile, user);

                int count = _context.Lessons.Where(m => m.TeacherId == userId & m.AcademyYear == model.AcademyYear & m.Month == model.Month).Count();

                var lesson = new Lesson
                {
                    Id = model.Id,
                    LessonTitle = model.LessonTitle,
                    Discreption = model.Discreption,
                    AcademyYear = model.AcademyYear,
                    Month = model.Month,
                    TeacherId = userId,
                    ThumbnailUrl = imageUrl,
                    VedioUrl = vedioUrl,
                    Numbre = count + 1
                };
                _context.Lessons.Add(lesson);
                _context.SaveChanges();

                return lesson;
            }
            return null;
        }

        #endregion

        #region Add Lesson Task

        public async Task<LessonTask> AddLessonTask(LessonTask model)
        {
            if (model != null)
            {
                var task = new LessonTask
                {
                    LessonId = model.LessonId,
                    Question = model.Question,
                };

                _context.lessonTasks.Add(task);
                _context.SaveChanges();

                return task;
            }
            return null;
        }

        #endregion

        #region Get Teacher Lessons List

        public async Task<IEnumerable<Lesson>> GetTeacherLessonsList()
        {
            var userId = await _userRepository.GitLoggingUserId();

            var lessons = _context.Lessons.Where(m => m.TeacherId == userId).ToList().OrderBy(m => m.AcademyYear);
            return lessons;
        }

        public async Task<IEnumerable<Lesson>> GetTeacherLessonsList(string teacherId, string year, string month)
        {
            var lessons = _context.Lessons.Where(m => m.TeacherId == teacherId & m.AcademyYear == year & m.Month == month).ToList();
            return lessons;
        }

        #endregion

        #region Get Lesson Task

        public async Task<IEnumerable<LessonTask>> GetLessonTask(string lessinId)
        {
            if(lessinId != null)
            {
                var task = _context.lessonTasks.Where(m => m.LessonId == lessinId).ToList();
                if(task != null)
                    return task;
            }
            return null;
        }

        #endregion

        #region Get Students Task List

        public async Task<IEnumerable<Students_Task>> GetStudentsTaskList(Lesson model)
        {
            if(model != null)
            {
                var tasks = _context.StudentTasks.Where(m => m.LessonId == model.Id).ToList();
                return tasks;
            }
            return null;
        }

        #endregion

        #region Upload Student Task

        public async Task<string> UploadStudentTask(LessonTsakVM model)
        {
            if(model != null)
            {
                var userId = await _userRepository.GitLoggingUserId();
                string username = _context.Users.Where(m => m.Id == userId).Select(m => m.UserName).FirstOrDefault();
                var student = _context.student.Find(userId);
                string name = student.FristName + "-" + student.LastName;

                var file = new Tools();
                string taskUrl = file.AddTasks(model.taskFile, name);

                var task = new Students_Task
                {
                    FileUrl = taskUrl,
                    LessonId = model.LessonId,
                    StudentId = userId
                };

                _context.StudentTasks.Add(task);
                _context.SaveChanges();

                return "Done";
            }
            return "Error";
        }

        #endregion

        #region Get Lesson Details

        public async Task<Lesson> GetLessonDetails(Lesson model)
        {
            if(model != null)
            {
                var lesson = _context.Lessons.Find(model.Id);
                return lesson;
            }
            return null;
        }

        #endregion

        #region Update Lesson Details

        public async Task<Lesson> UpdateLessonDetails(LessonVM model)
        {
            if(model != null)
            {
                var userId = await _userRepository.GitLoggingUserId();
                var user = _context.Users.Where(m => m.Id == userId).Select(m => m.UserName).FirstOrDefault();
                var lesson = _context.Lessons.Find(model.Id);

                lesson.Id = model.Id;
                lesson.LessonTitle = model.LessonTitle;
                lesson.Discreption = model.Discreption;
                lesson.AcademyYear = model.AcademyYear;
                lesson.Month = model.Month;
                if (model.VedioFile != null)
                {
                    var vedio = new Tools();
                    string vedioUrl = vedio.AddVedios(model.VedioFile, user);

                    lesson.VedioUrl = vedioUrl;
                }
                else
                {
                    lesson.VedioUrl = lesson.VedioUrl;
                }

                if (model.ImageFile != null)
                {
                    var image = new Tools();
                    string imageUrl = image.AddImages(model.ImageFile, user);

                    lesson.ThumbnailUrl = imageUrl;
                }
                else
                {
                    lesson.ThumbnailUrl = lesson.ThumbnailUrl;
                }

                _context.Update(lesson);
                _context.SaveChanges();

                return lesson;
            }
            return null;
        }

        #endregion

    }
}
