using ArabityAuth;
using CenterManagement.Data;
using CenterManagement.Models;
using CenterManagement.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CenterManagement.Controllers
{
    public class Courses : Controller
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private object returnUrl;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _Environment;

        public Courses(ApplicationDbContext context,
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

        public IActionResult Months(TeacherIdVM model)
        {
            ViewBag.TeacherId = model.Id;
            ViewBag.Year = model.year;

            return View();
        }

        public IActionResult Monthly_Enroll_Barcode(Lesson model)
        {
            ViewBag.Month = model.Month;
            ViewBag.Year = model.AcademyYear;
            ViewBag.TeacherId = model.TeacherId;

            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var check = _context.Barcodes.Where(m => m.StudendId == userId & m.AcademyYear == model.AcademyYear & m.Month == model.Month).FirstOrDefault();
            if (check != null)
            {

                ViewBag.Year = model.AcademyYear;
                ViewBag.Month = model.Month;
                var teacher = _context.teachers.Find(model.TeacherId);
                ViewBag.Teacher = teacher;
                var lessons = _context.Lessons.Where(m => m.TeacherId == model.TeacherId & m.AcademyYear == model.AcademyYear & m.Month == model.Month).ToList();
                ViewBag.Lessons = lessons;

                return View("Monthly_Lessons");
            }

            return View();
        }

        public IActionResult Monthly_Lessons(MonthlyBarcodeVM model)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //var check = _context.Barcodes.Where(m => m.barcode == model.Barcode & m.AcademyYear == model.AcademyYear & m.Month == model.Month).FirstOrDefault(); 
            var check = _context.Barcodes.Find(model.Barcode);
            if (check == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                if (check.AcademyYear == model.AcademyYear)
                {
                    if (check.Month == model.Month)
                    {
                        if (check.StudendId == "#")
                        {
                            check.StudendId = userId;
                            _context.Barcodes.Update(check);
                            _context.SaveChanges();
                        }
                        else if (check.StudendId != userId)
                        {
                            return RedirectToAction("Index", "Home");
                        }

                        ViewBag.Year = model.AcademyYear;
                        ViewBag.Month = model.Month;
                        var teacher = _context.teachers.Find(model.TeacherId);
                        ViewBag.Teacher = teacher;

                        var lessons = _context.Lessons.Where(m => m.TeacherId == model.TeacherId & m.AcademyYear == model.AcademyYear & m.Month == model.Month).ToList();
                        ViewBag.Lessons = lessons;

                        return View();
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            
            } 
        }

        public IActionResult Lesson_Details(Lesson model)
        {
            var lesson = _context.Lessons.Find(model.Id);
            ViewBag.Lesson = lesson;

            return View();
        }

        [HttpGet]
        public IActionResult Lesson_Task(Lesson model)
        {
            var tasks = _context.lessonTasks.Where(m => m.LessonId == model.Id);
            ViewBag.Tasks = tasks;

            ViewBag.LessonId = model.Id;

            return View();
        }

        [HttpPost]
        public IActionResult Save_Lesson_Task([FromForm] LessonTsakVM model)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            string username = _context.Users.Where(m => m.Id == userId).Select(m => m.UserName).FirstOrDefault();
            var student = _context.student.Find(userId);
            string name = student.FristName + "-" + student.LastName;

            var image = new Tools(_Environment);
            string taskUrl = image.AddTasks(model.taskFile, name);

            var task = new Students_Task
            {
                FileUrl = taskUrl,
                LessonId = model.LessonId,
                StudentId = userId
            };

            _context.StudentTasks.Add(task);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Add_Lesson_Task(Lesson model)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Save_Task(LessonTask model)
        {
            var task = new LessonTask
            {
                LessonId = model.LessonId,
                Question = model.Question,
            };

            _context.lessonTasks.Add(task);
            _context.SaveChanges();

            return View("Add_Lesson_Task");
        }

        public IActionResult Teacher_Barcodes()
        {
            return View();
        }
     
        public IActionResult Choose_Month(AcademYearVM model)
        {
            ViewBag.year = model.AcademyYear;

            return View();
        }

        public IActionResult Get_Barcodes(AcademYearVM model)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var barcodes = _context.Barcodes.Where(m => m.TeacherId == userId & m.AcademyYear == model.AcademyYear & m.Month == model.Month).ToList();
            ViewBag.Barcodes = barcodes;

            return View();
        }
    }
}
