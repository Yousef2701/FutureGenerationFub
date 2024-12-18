using CenterManagement.IRepository;
using CenterManagement.Models;
using CenterManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CenterManagement.Controllers
{
    public class Admin : Controller
    {

        #region Dependancey injuction

        private readonly IStudentRepository _studentRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBarcodeRepository _barcodeRepository;

        public Admin(IStudentRepository studentRepository,
                     ITeacherRepository teacherRepository,
                     IUserRepository userRepository,
                     IBarcodeRepository barcodeRepository)
        {
            _studentRepository = studentRepository;
            _teacherRepository = teacherRepository;
            _userRepository = userRepository;
            _barcodeRepository = barcodeRepository;
        }

        #endregion


        #region Admin Page

        public async Task<IActionResult> AdminPage()
        {
            ViewBag.Students = await _studentRepository.GetStudentsCount();
            ViewBag.Teachers = await _teacherRepository.GetTeachersCount();
            ViewBag.Padding = await _userRepository.GetPaddingAccountsCount();
            int count = await _barcodeRepository.GetBarcodesCount();
            ViewBag.Count = count * 20;

            return View();
        }

        #endregion

        #region Enroll Barcode

        [HttpGet]
        public async Task<IActionResult> EnrollBarcode()
        {
            ViewBag.Teachers = await _teacherRepository.GetTeachersList();

            return View();
        }

        #endregion

        #region Create Barcodes

        public async Task<IActionResult> CreateBarcodes(EnrollParcodeVM model)
        {
            ViewBag.Parcodes = await _barcodeRepository.CreateBarcodes(model);

            return View();
        }

        #endregion

        #region Students List

        public async Task<IActionResult> StudentsList()
        {
            ViewBag.Students = await _studentRepository.GetStudentsList();

            return View();
        }

        #endregion

        #region Student Page

        public async Task<IActionResult> StudentPage(Student model)
        {
            ViewBag.Student = await _studentRepository.GetStudentData(model.Id);

            return View();
        }

        #endregion

        #region PaddingStudent

        public async Task<IActionResult> PaddingStudent(Student model)
        {
            var result = await _userRepository.PaddingUserAccount(model.Id);

            return RedirectToAction("StudentsList");
        }

        #endregion

        #region Delete Student

        public async Task<IActionResult> DeleteStudent(Student model)
        {
            var result = await _studentRepository.DeleteStudent(model);

            return RedirectToAction("StudentsList");
        }

        #endregion

        #region Teachers List

        public async Task<IActionResult> TeachersList()
        {
            ViewBag.Teachers = await _teacherRepository.GetTeachersList();

            return View();
        }

        #endregion

        #region Teacher Page

        public async Task<IActionResult> TeacherPage(Teacher model)
        {
            @ViewBag.Teacher = await _teacherRepository.GetTeacherData(model.Id);

            return View();
        }

        #endregion

        #region Padding Teacher

        public async Task<IActionResult> PaddingTeacher(Teacher model)
        {
            var result = await _userRepository.PaddingUserAccount(model.Id);

            return RedirectToAction("TeachersList");
        }

        #endregion

        #region Delete Teacher

        public async Task<IActionResult> DeleteTeacher(Teacher model)
        {
            var result = await _teacherRepository.DeleteTeacher(model);

            return RedirectToAction("TeachersList");
        }

        #endregion

        #region Earned Money

        public async Task<IActionResult> EarnedMoney()
        {
            return View();
        }

        #endregion

        #region Pending Accounts

        public async Task<IActionResult> PendingAccounts()
        {
            ViewBag.Usernames = await _userRepository.GetPaddingUsersList();

            return View();
        }

        #endregion

        #region Active Accounts

        public async Task<IActionResult> ActiveAccounts(UsernameVM model)
        {
            var result = _userRepository.ActiveUserAccount(model);

            return RedirectToAction("PendingAccounts");
        }

        #endregion

        
    }
}
