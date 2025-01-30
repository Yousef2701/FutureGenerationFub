using System.Security.Claims;
using CenterManagement.IRepository;
using CenterManagement.Models;
using CenterManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CenterManagement.Controllers
{
    public class StudentManagement : Controller
    {

        #region Dependancey injuction

        private readonly IStudentRepository _studentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IExamRepository _examRepository;

        public StudentManagement(IStudentRepository studentRepository,
                                 IUserRepository userRepository,
                                 IExamRepository examRepository)
        {
            _studentRepository = studentRepository;
            _userRepository = userRepository;
            _examRepository = examRepository;
        }

        #endregion


        #region Complete Student Data

        [HttpGet]
        public async Task<IActionResult> CompleteStudentData()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CompleteStudentData([FromForm] FinishSDataVM model)
        {
            var result = _studentRepository.AddNewStudent(model);

            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region Personal Page

        [HttpGet]
        public async Task<IActionResult> PersonalPage()
        {
            ViewBag.Student = await _studentRepository.GetStudentData(await _userRepository.GitLoggingUserId());

            return View();
        }

        #endregion

        #region Update Student Data

        [HttpGet]
        public async Task<IActionResult> UpdateStudentData()
        {
            ViewBag.Student = await _studentRepository.GetStudentData(await _userRepository.GitLoggingUserId());

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStudentData([FromForm] UpdateStudentDataVM model)
        {
            var result = await _studentRepository.UpdateStudentData(model);

            return RedirectToAction("PersonalPage");
        }

        #endregion

        #region Student Exams List

        [HttpGet]
        public async Task<IActionResult> StudentExamsList()
        {
            ViewBag.Exams = await _examRepository.GetStudentExamsList();

            return View();
        }

        #endregion

        #region Delete Student Account

        [HttpDelete]
        public async Task<IActionResult> DeleteStudentAccount()
        {
            var result = await _studentRepository.DeleteStudent();

            return RedirectToAction("Index", "Home");
        }

        #endregion

    }
}
