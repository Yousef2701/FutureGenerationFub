using ArabityAuth;
using CenterManagement.Controllers;
using CenterManagement.Data;
using CenterManagement.Models;
using CenterManagement.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Security.Claims;

namespace Online_Exams.Controllers
{
    public class Teachers : Controller
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private object returnUrl;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _Environment;

        public Teachers(ApplicationDbContext context,
                        IHttpContextAccessor httpContextAccessor,
                        UserManager<IdentityUser> userManager,
                        ILogger<HomeController> logger,
                        SignInManager<IdentityUser> signInManager,
                        Microsoft.AspNetCore.Hosting.IHostingEnvironment Environment)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _logger = logger;
            _signInManager = signInManager;
            _Environment = Environment;
        }

        [HttpGet]
        public IActionResult CompleteTData()
        {
            return View();
        }

        [HttpPost]
        public IActionResult FinishTData([FromForm] FinishTDataVM model)
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

            return RedirectToAction("Index", "Home");
        }

        public IActionResult PersonalPage()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var teacher = _context.teachers.Where(m => m.Id == userId).FirstOrDefault();
            ViewBag.Teacher = teacher;

            return View();
        }

        [HttpGet]
        public IActionResult UpdateTeacherData()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var teacher = _context.teachers.Where(m => m.Id == userId).FirstOrDefault();
            ViewBag.Teacher = teacher;

            return View();
        }

        [HttpPost]
        public IActionResult UpdateTeacherData([FromForm] UpdateTeacherDataVM model)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var CurrentTeacher = _context.teachers.Where(m => m.Id == userId).FirstOrDefault();

            var username = _context.Users.Where(m => m.Id == userId).Select(o => o.UserName).FirstOrDefault();
            var image = new Tools(_Environment);
            string imageUrl = image.AddImages(model.ImageFile, username);

            CurrentTeacher.Id = userId;
            CurrentTeacher.FristName = model.FristName;
            CurrentTeacher.LastName = model.LastName;
            CurrentTeacher.Subject = model.Subject;
            CurrentTeacher.PhoneNumber = model.PhoneNumber;
            CurrentTeacher.Email = model.Email;
            CurrentTeacher.ImageUrl = imageUrl;
            CurrentTeacher.Governorate = model.Governorate;
            CurrentTeacher.City = model.City;

            _context.teachers.Update(CurrentTeacher);
            _context.SaveChanges();

            return RedirectToAction("PersonalPage", "Teachers");
        }

        [HttpGet]
        public IActionResult AddNewLesson()
        {
             return View();
        }

        [HttpPost]
        public IActionResult AddNewLesson([FromForm] LessonVM model)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
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

            return RedirectToAction("Add_Lesson_Task","Courses");
        }

        public IActionResult MyLessons()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var lessons = _context.Lessons.Where(m => m.TeacherId == userId).ToList().OrderBy(m => m.AcademyYear);
            ViewBag.Lessons = lessons;

            return View();
        }

        public IActionResult Git_Lesson_Tasks(Lesson model)
        {
            var tasks = _context.StudentTasks.Where(m => m.LessonId == model.Id).ToList();
            ViewBag.Tasks = tasks;

            return View();
        }

        [HttpGet]
        public IActionResult LessonDetails(Lesson model)
        {
            var lesson = _context.Lessons.Find(model.Id);
            ViewBag.Lesson = lesson;

            return View();
        }

        [HttpGet]
        public IActionResult Update_Lesson(Lesson model)
        {
            var lesson = _context.Lessons.Find(model.Id);
            ViewBag.Lesson = lesson;

            return View();
        }

        [HttpPost]
        public IActionResult Update_Lesson(LessonVM model)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = _context.Users.Where(m => m.Id == userId).Select(m => m.UserName).FirstOrDefault();

            var vedio = new Tools(_Environment);
            string vedioUrl = vedio.AddVedios(model.VedioFile, user);

            var image = new Tools(_Environment);
            string imageUrl = image.AddImages(model.ImageFile, user);

            var lesson = _context.Lessons.Find(model.Id);
            string v = lesson.VedioUrl;
            string i = lesson.ThumbnailUrl;

            lesson.Id = model.Id;
            lesson.LessonTitle = model.LessonTitle;
            lesson.Discreption = model.Discreption;
            lesson.AcademyYear = model.AcademyYear;
            lesson.Month = model.Month;
            if(model.VedioFile != null)
            {
                lesson.VedioUrl = vedioUrl;
            }
            else
            {
                lesson.VedioUrl = v;
            }

            if(model.ImageFile !=null)
            {
                lesson.ThumbnailUrl = imageUrl;
            }
            else
            {
                lesson.ThumbnailUrl = i;
            }

            _context.Update(lesson);
            _context.SaveChanges();

            return RedirectToAction("MyLessons");
        }

        public IActionResult Category(CategoryVM model)
        {
            List<Teacher> teachers = _context.teachers.ToList();
            ViewBag.teachers = teachers;
            ViewBag.Category = model.Category;

            return View();
        }

        //[HttpGet]
        //public IActionResult Teacher_Page(Teacher model)
        //{
        //    var teacher = _context.teachers.Where(m => m.Id == model.Id).FirstOrDefault();
        //    ViewBag.teacher = teacher;
        //    return View();
        //}

        //public IActionResult ContactTeacher()
        //{
        //    return View();
        //}

        public IActionResult SystemTracks(Teacher model)
        {
            ViewBag.id = model.Id;

            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var student = _context.student.Find(userId);
            ViewBag.year = student.AcademyYear;

            return View();
        }

        public IActionResult ChooseExams(TeacherIdVM model)
        {
            var exams = _context.Exams.Where(m => m.TeacherId == model.Id & m.AcademyYear== model.year).ToList();
            ViewBag.exams = exams;

            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            ViewBag.User = userId;

            return View();
        }

        [HttpGet]
        public IActionResult Exam(Exam model)
        {
            var questions = _context.Questions.Where(m => m.ExamId== model.Id).ToList();
            string q = "";
            string ans = "";
            int c = 0;

            foreach(Question item in questions)
            {
                c++;
                q += item.Quest;

                if (c < questions.Count) 
                { 
                    q+= ", ";
                }

                string answer1 = item.FristAnswer;
                string answer2 = item.SecondAnswer;
                string answer3 = item.ThirdAnswer;
                string answer4 = item.ForthAnswer;
                string t = answer1 + "," + answer2 + "," + answer3 + "," + answer4;

                ans += t ;

                if (c < questions.Count)
                {
                    ans += " && ";
                }

            }

            ViewBag.Questions = q;
            ViewBag.ans = ans;
            ViewBag.Min = _context.Exams.Where(m => m.Id == model.Id).Select(m => m.Minutes).FirstOrDefault();
            ViewBag.Exam = _context.Exams.Find(model.Id);

            return View();
        }

        [HttpGet]
        public IActionResult Exam_Answers(ExamVM model)
        {
            string[] ans = model.Answers.Split(",");
            var questions = _context.Questions.Where(m => m.ExamId == model.ExamId).ToList();

            int c1 = 0;
            int index = -1;

            string logic = "[";
            foreach (Question q in questions)
            {
                c1++;
                index++;

                logic += "{ \"question\": \"";
                logic += q.Quest;
                logic += "\",\"choices\": [";
                    
                logic += "\"";
                logic += q.FristAnswer;
                logic += "\",\"";
                logic += q.SecondAnswer;
                logic += "\",\"";
                logic += q.ThirdAnswer;
                logic += "\",\"";
                logic += q.ForthAnswer;
                logic += "\"], \"correctAnswer\" :";
                if(q.CorrectAnswer == "Frist")
                {
                    logic += "0 , \"studentAnswer\" :";
                }
                else if (q.CorrectAnswer == "Second")
                {
                    logic += "1 , \"studentAnswer\" :";
                }
                else if (q.CorrectAnswer == "Third")
                {
                    logic += "2 , \"studentAnswer\" :";
                }
                else if (q.CorrectAnswer == "Fourth")
                {
                    logic += "3 , \"studentAnswer\" :";
                }

                string studentAns = ans[index].ToString();

                if (studentAns.ToLower().Replace(" ","") == q.FristAnswer.ToLower().Replace(" ", ""))
                {
                    logic += "0}";
                }
                else if (studentAns.ToLower().Replace(" ", "") == q.SecondAnswer.ToLower().Replace(" ", ""))
                {
                    logic += "1}";
                }
                else if (studentAns.ToLower().Replace(" ", "") == q.ThirdAnswer.ToLower().Replace(" ", ""))
                {
                    logic += "2}";
                }
                else if (studentAns.ToLower().Replace(" ", "") == q.ForthAnswer.ToLower().Replace(" ", ""))
                {
                    logic += "3}";
                }

                if(c1 < questions.Count)
                {
                    logic += ",";
                }

            }

            logic += "]";

            ViewBag.Logic = logic;
            ViewBag.ExamId = model.ExamId;

            return View();
        }

        [HttpPost]
        public IActionResult Save_Result(ExamAnswersVM model)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int count = _context.Questions.Where(m => m.ExamId == model.ExamId).Count();
            int w = count - Convert.ToInt32(model.correct);

            var result = new Result
            {
                StudentId = userId,
                ExamId = model.ExamId,
                Date = DateTime.Now.ToString("dd-MM-yyyy"),
                Time = DateTime.Now.ToString("hh:mm tt"),
                CorrectAnswers = model.correct,
                WrongAnswers = w
            };

            _context.Results.Add(result);
            _context.SaveChanges();

            return RedirectToAction("Index","Home");
        }

        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
