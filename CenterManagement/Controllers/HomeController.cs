using CenterManagement.Data;
using CenterManagement.IRepository;
using CenterManagement.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CenterManagement.Controllers
{
    public class HomeController : Controller
    {

        #region Dependancey injuction

        private readonly ITeacherRepository _teacherRepository;

        public HomeController(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        #endregion


        #region Index

        public async Task<IActionResult> Index()
        {
            ViewBag.teachers = await _teacherRepository.GetTeachersList();
            return View();
        }

        #endregion

        #region Contact Us

        public async Task<IActionResult> ContactUs()
        {
            return View();
        }

        #endregion

        #region About Us

        public async Task<IActionResult> AboutUs()
        {
            return View();
        }

        #endregion

        #region Error

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #endregion

    }
}