using CenterManagement.Data;
using CenterManagement.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using CenterManagement.ViewModels;

namespace CenterManagement.Controllers
{
    public class Exams : Controller
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private object returnUrl;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _Environment;

        public Exams(ApplicationDbContext context,
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
        public IActionResult AddExam()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddExam(Exam model)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

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

            return RedirectToAction("AddQuestion");
        }

        [HttpGet]
        public IActionResult AddQuestion()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddQuestion(Question model)
        {
            int count = _context.Questions.Where(m => m.ExamId== model.ExamId).Count();

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

            return View();
        }

        [HttpGet]
        public IActionResult TeacherExams()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var exams = _context.Exams.Where(m => m.TeacherId == userId).ToList();
            ViewBag.Exams = exams;

            return View();
        }

        [HttpGet]
        public IActionResult ExamDetails(Exam model)
        {
            var questions = _context.Questions.Where(m => m.ExamId == model.Id).ToList();
            ViewBag.Questions = questions;

            return View();
        }

        [HttpGet]
        public IActionResult ExamStudents(Exam model)
        {
            var res = _context.Results.Where(m => m.ExamId == model.Id).ToList();
            ViewBag.Results = res;

            int count = _context.Questions.Where(m => m.ExamId == model.Id).Count();
            ViewBag.Count = count;

            return View();
        }

        [HttpGet]
        public IActionResult UpdateQuestion(Question model)
        {
            var question = _context.Questions.Where(m => m.ExamId == model.ExamId & m.Numbre == model.Numbre).FirstOrDefault();
            ViewBag.Question = question;

            return View();
        }

        [HttpPost]
        public IActionResult UpdateQuestion(QuestionVM model)
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

            return RedirectToAction("TeacherExams");
        }

        public IActionResult RemoveQuestion(Question model)
        {
            var questions = _context.Questions.Where(m => m.ExamId == model.ExamId).ToList();
            foreach(Question question in questions)
            {
                if(question.Numbre == model.Numbre)
                {
                    _context.Questions.Remove(question);
                    _context.SaveChanges();
                }
            }

            return RedirectToAction("TeacherExams");
        }
    }
}
