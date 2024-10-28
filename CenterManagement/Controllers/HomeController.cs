using CenterManagement.Data;
using CenterManagement.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CenterManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private object returnUrl;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _Environment;

        public HomeController(ApplicationDbContext context,
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

        public IActionResult Index()
        {
            List<Teacher> teachers = _context.teachers.ToList();
            ViewBag.teachers = teachers;
            return View();
        }

        public IActionResult Contact_Us()
        {
            return View();
        }

        public IActionResult About_Us()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}