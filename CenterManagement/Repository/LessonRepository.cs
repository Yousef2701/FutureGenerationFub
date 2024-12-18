﻿using ArabityAuth;
using System.Security.Claims;
using CenterManagement.Data;
using CenterManagement.IRepository;
using CenterManagement.Models;
using CenterManagement.ViewModels;

namespace CenterManagement.Repository
{
    public class LessonRepository : ILessonRepository
    {

        #region Dependancey injuction

        private readonly ApplicationDbContext _context;
        private readonly IUserRepository _userRepository;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _Environment;

        public LessonRepository(ApplicationDbContext context,
                                IUserRepository userRepository,
                                Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _context = context;
            _userRepository = userRepository;
            _Environment = environment;
        }

        #endregion


        #region Add New Lesson

        public async Task<Lesson> AddNewLesson(LessonVM model)
        {
            if(model != null)
            {
                var userId = await _userRepository.GitLoggingUserId();
                var user = _context.Users.Where(m => m.Id == userId).Select(m => m.UserName).FirstOrDefault();

                var vedio = new Tools(_Environment);
                string vedioUrl = vedio.AddVedios(model.VedioFile, user);

                var image = new Tools(_Environment);
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
                    var vedio = new Tools(_Environment);
                    string vedioUrl = vedio.AddVedios(model.VedioFile, user);

                    lesson.VedioUrl = vedioUrl;
                }
                else
                {
                    lesson.VedioUrl = lesson.VedioUrl;
                }

                if (model.ImageFile != null)
                {
                    var image = new Tools(_Environment);
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
