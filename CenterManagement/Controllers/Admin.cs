using CenterManagement.Data;
using CenterManagement.Models;
using CenterManagement.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CenterManagement.Controllers
{
    public class Admin : Controller
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private object returnUrl;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _Environment;

        public Admin(ApplicationDbContext context,
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


        public IActionResult AdminPage()
        {
            ViewBag.Students = _context.student.Count();
            ViewBag.Teachers = _context.teachers.Count();
            ViewBag.Padding = _context.Users.Where(m => m.EmailConfirmed == false).Count();
            int count = _context.Barcodes.Count();
            ViewBag.Count = count * 20;

            return View();
        }

        public IActionResult Students()
        {
            var students = _context.student.ToList().OrderBy(m => m.FristName);
            ViewBag.Students = students;

            return View();
        }

        public IActionResult Student_Page(Student model)
        {
            var student = _context.student.Find(model.Id);
            ViewBag.Student = student;

            return View();
        }

        public IActionResult Padding_Student(Student model)
        {
            var student = _context.Users.Find(model.Id);
            student.EmailConfirmed = false;
            _context.Update(student);
            _context.SaveChanges();

            return RedirectToAction("Students");
        }

        public IActionResult Delete_Student(Student model)
        {
            var student = _context.student.Find(model.Id);

            _context.student.Remove(student);
            
            var role = _context.UserClaims.Where(m => m.UserId == model.Id).FirstOrDefault();
            _context.UserClaims.Remove(role);

            var user = _context.Users.Where(m => m.Id == model.Id).FirstOrDefault();
            _context.Users.Remove(user);

            _context.SaveChanges();

            return RedirectToAction("Students");
        }

        public IActionResult Teachers()
        {
            var teachers = _context.teachers.ToList().OrderBy(m => m.FristName);
            ViewBag.Teachers = teachers;

            return View();
        }

        public IActionResult Teacher_Page(Teacher model)
        {
            @ViewBag.Teacher = _context.teachers.Find(model.Id);

            return View();
        }

        public IActionResult Padding_Teacher(Teacher model)
        {
            var teacher = _context.Users.Find(model.Id);
            teacher.EmailConfirmed = false;
            _context.Update(teacher);
            _context.SaveChanges();

            return RedirectToAction("Teachers");
        }

        public IActionResult Delete_Teacher(Teacher model)
        {
            var exams = _context.Exams.Where(m => m.TeacherId == model.Id).ToList();
            foreach(var exam in exams)
            {
                var results = _context.Results.Where(m => m.ExamId == exam.Id).ToList();
                foreach(var result in results)
                {
                    _context.Results.Remove(result);
                    _context.SaveChanges();
                }

                _context.Exams.Remove(exam);
                _context.SaveChanges();
            }

            var teacher = _context.teachers.Find(model.Id);
            _context.teachers.Remove(teacher);

            var role = _context.UserClaims.Where(m => m.UserId == model.Id).FirstOrDefault();
            _context.UserClaims.Remove(role);

            var user = _context.Users.Find(model.Id);
            _context.Users.Remove(user);

            _context.SaveChanges();

            return RedirectToAction("Teachers");
        }

        public IActionResult Earned_Money()
        {
            return View();
        }

        public IActionResult Pending_Accounts()
        {
            var padding = _context.Users.Where(m => m.EmailConfirmed != true).ToList();
            List<string> usernames = new List<string>();

            foreach(var user in padding)
            {
                string username = _context.Users.Where(m => m.Id == user.Id).Select(m => m.UserName).FirstOrDefault();
                usernames.Add(username);
            }

            ViewBag.Usernames = usernames;

            return View();
        }

        public IActionResult Active_Accounts(UsernameVM model)
        {
            var user = _context.Users.Where(m => m.UserName == model.Username).FirstOrDefault();
            if (user != null)
            {
                user.EmailConfirmed = true;
                _context.Users.Update(user);
                _context.SaveChanges();
            }

            return RedirectToAction("Pending_Accounts");
        }

        [HttpGet]
        public IActionResult Enroll_Parcode()
        {
            var teachers = _context.teachers.ToList();
            ViewBag.Teachers = teachers;

            return View();
        }

        public IActionResult CreateParcodes(EnrollParcodeVM model)
        {
            int n = Convert.ToInt32(model.Numbre);

            Random random = new Random();
            int[] randomNumbers = new int[n];

            for (int i = 0; i < n; i++)
            {
                randomNumbers[i] = random.Next(10000000, 99999999);

                var barcode = new Barcode
                {
                    barcode = randomNumbers[i].ToString(),
                    TeacherId = model.TeacherId,
                    AcademyYear = model.AcademyYear,
                    Month = model.Month,
                    StudendId = "#"
                };
                _context.Barcodes.Add(barcode);
                _context.SaveChanges();
            }

            ViewBag.Parcodes = randomNumbers;

            return View();
        }
    }
}
