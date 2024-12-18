using System.Security.Claims;
using ArabityAuth;
using CenterManagement.Data;
using CenterManagement.IRepository;
using CenterManagement.Models;
using CenterManagement.ViewModels;

namespace CenterManagement.Repository
{
    public class TeacherRepository : ITeacherRepository
    {

        #region Dependancey injuction

        private readonly ApplicationDbContext _context;
        private readonly IUserRepository _userRepository;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _Environment;

        public TeacherRepository(ApplicationDbContext context,
                                 IUserRepository userRepository,
                                 Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _context = context;
            _userRepository = userRepository;
            _Environment = environment;
        }

        #endregion


        #region Add New Teacher

        public async Task<Teacher> AddNewTeacher(FinishTDataVM model)
        {
            if(model != null)
            {
                var image = new Tools(_Environment);
                string imageUrl = image.AddImages(model.ImageFile, model.Username);

                string id = _context.Users.Where(m => m.Email == model.Username).Select(m => m.Id).FirstOrDefault();
                var teacher = new Teacher
                {
                    Id = id,
                    FristName = model.FristName,
                    LastName = model.LastName,
                    Subject = model.Subject,
                    PhoneNumber = model.PhoneNumber,
                    Email = model.Email,
                    ImageUrl = imageUrl,
                    Governorate = model.Governorate,
                    City = model.City
                };

                _context.teachers.Add(teacher);
                _context.SaveChanges();

                return teacher;
            }
            return null;
        }

        #endregion

        #region Get Logging Teacher Data

        public async Task<Teacher> GetLoggingTeacherData()
        {
            var userId = await _userRepository.GitLoggingUserId();

            var teacher = _context.teachers.Where(m => m.Id == userId).FirstOrDefault();
            return teacher;
        }

        #endregion

        #region Update Teacher Data

        public async Task<Teacher> UpdateTeacherData(UpdateTeacherDataVM model)
        {
            if(model != null)
            {
                var userId = await _userRepository.GitLoggingUserId();
                var CurrentTeacher = _context.teachers.Where(m => m.Id == userId).FirstOrDefault();

                if(model.ImageFile != null)
                {
                    var username = _context.Users.Where(m => m.Id == userId).Select(o => o.UserName).FirstOrDefault();
                    var image = new Tools(_Environment);
                    string imageUrl = image.AddImages(model.ImageFile, username);

                    CurrentTeacher.ImageUrl = imageUrl;
                }
                else
                {
                    CurrentTeacher.ImageUrl = CurrentTeacher.ImageUrl;
                }
                

                CurrentTeacher.Id = userId;
                CurrentTeacher.FristName = model.FristName;
                CurrentTeacher.LastName = model.LastName;
                CurrentTeacher.Subject = model.Subject;
                CurrentTeacher.PhoneNumber = model.PhoneNumber;
                CurrentTeacher.Email = model.Email;
                CurrentTeacher.Governorate = model.Governorate;
                CurrentTeacher.City = model.City;

                _context.teachers.Update(CurrentTeacher);
                _context.SaveChanges();

                return CurrentTeacher;
            }
            return null;
        }

        #endregion

        #region Get Teachers Count

        public async Task<int> GetTeachersCount()
        {
            return _context.teachers.Count();
        }

        #endregion

        #region Get Teachers List

        public async Task<IEnumerable<Teacher>> GetTeachersList()
        {
            return _context.teachers.OrderBy(m => m.FristName).ThenBy(m => m.LastName).ToList();
        }

        #endregion

        #region Get Teacher Data

        public async Task<Teacher> GetTeacherData(string teacherId)
        {
            if(teacherId != null)
            {
                return _context.teachers.Find(teacherId);
            }
            return null;
        }

        #endregion

        #region Delete Teacher

        public async Task<Teacher> DeleteTeacher(Teacher model)
        {
            if(model != null)
            {
                var exams = _context.Exams.Where(m => m.TeacherId == model.Id).ToList();
                if (exams != null)
                {
                    foreach (var exam in exams)
                    {
                        var questions = _context.Questions.Where(m => m.ExamId == exam.Id).ToList();
                        if (questions != null)
                        {
                            foreach (var question in questions)
                            {
                                _context.Questions.Remove(question);
                                _context.SaveChanges();
                            }
                        }

                        var results = _context.Results.Where(m => m.ExamId == exam.Id).ToList();
                        foreach (var result in results)
                        {
                            _context.Results.Remove(result);
                            _context.SaveChanges();
                        }

                        _context.Exams.Remove(exam);
                        _context.SaveChanges();
                    }
                }

                var lessons = _context.Lessons.Where(m => m.TeacherId == model.Id).ToList();
                if (lessons != null)
                {
                    foreach (var lesson in lessons)
                    {
                        var tasks = _context.lessonTasks.Where(m => m.LessonId == lesson.Id).ToList();
                        if (tasks != null)
                        {
                            foreach (var task in tasks)
                            {
                                _context.lessonTasks.Remove(task);
                                _context.SaveChanges();
                            }
                        }

                        _context.Lessons.Remove(lesson);
                        _context.SaveChanges();
                    }
                }

                var barcodes = _context.Barcodes.Where(m => m.TeacherId == model.Id).ToList();
                if (barcodes != null)
                {
                    foreach (var barcode in barcodes)
                    {
                        _context.Barcodes.Remove(barcode);
                        _context.SaveChanges();
                    }
                }


                var teacher = _context.teachers.Find(model.Id);
                _context.teachers.Remove(teacher);

                var role = _context.UserClaims.Where(m => m.UserId == model.Id).FirstOrDefault();
                _context.UserClaims.Remove(role);

                var user = _context.Users.Find(model.Id);
                _context.Users.Remove(user);

                _context.SaveChanges();
            }
            return null;
        }

        #endregion

    }
}
