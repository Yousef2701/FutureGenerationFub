using ArabityAuth;
using CenterManagement.Data;
using CenterManagement.Models;
using CenterManagement.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using System.Security.Claims;

namespace CenterManagement.Controllers
{
    public class Students : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private object returnUrl;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _Environment;

        public Students(ApplicationDbContext context,
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
        public IActionResult CompleteSData()
        {
            return View();
        }

        [HttpPost]
        public IActionResult FinishSData([FromForm] FinishSDataVM model)
        {
            var image = new Tools(_Environment);
            string imageUrl = image.AddImages(model.ImageFile, model.Username);

            string id = _context.Users.Where(m => m.Email==model.Username).Select(m => m.Id).FirstOrDefault();
            var student = new Student
            {
                Id = id,
                FristName = model.FristName,
                LastName = model.LastName,
                AcademyYear = model.AcademyYear,
                PhoneNumber = model.PhoneNumber,
                GuardingPhoneNumber = model.GuardingPhoneNumber,
                Email = model.Email,
                Governorate = model.Governorate,
                City = model.City,
                ImageUrl = imageUrl
            };

            _context.student.Add(student);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult PersonalPage()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var student = _context.student.Where(m => m.Id == userId).FirstOrDefault();
            ViewBag.Student = student;

            return View();
        }

        [HttpGet]
         public IActionResult UpdateStudentData()
         {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var student = _context.student.Where(m => m.Id == userId).FirstOrDefault();
            ViewBag.Student = student;

            return View();
         }

        [HttpPost]
        public IActionResult UpdateStudentData([FromForm] UpdateStudentDataVM model)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var CurrentStudent = _context.student.Where(m => m.Id == userId).FirstOrDefault();

            var username = _context.Users.Where(m => m.Id == userId).Select(o => o.UserName).FirstOrDefault();
            var image = new Tools(_Environment);
            string imageUrl = image.AddImages(model.ImageFile, username);

            CurrentStudent.Id = userId;
            CurrentStudent.FristName = model.FristName;
            CurrentStudent.LastName = model.LastName;
            CurrentStudent.PhoneNumber = model.PhoneNumber;
            CurrentStudent.GuardingPhoneNumber = model.GuardingPhoneNumber;
            CurrentStudent.Email = model.Email;
            CurrentStudent.ImageUrl = imageUrl;
            CurrentStudent.Governorate = model.Governorate;
            CurrentStudent.City = model.City;
            CurrentStudent.AcademyYear = model.AcademyYear;

            _context.student.Update(CurrentStudent);
            _context.SaveChanges();

            return RedirectToAction("PersonalPage", "Students");
        }

        public IActionResult AllExams()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var exams = _context.Results.Where(m => m.StudentId == userId).ToList();
            ViewBag.Exams = exams;

            return View();
        }
        
       
        public async Task<IActionResult> DeleteStudentAccount()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");

            if (userId != null)
            {

                var student = _context.student.Find(userId);
                _context.student.Remove(student);

                var cl = _context.UserClaims.Where(m => m.UserId == userId).FirstOrDefault();
                _context.UserClaims.Remove(cl);

                var user = _context.Users.Find(userId);
                _context.Users.Remove(user);

                _context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
