using System.Security.Claims;
using CenterManagement.IRepository;
using CenterManagement.Models;
using CenterManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CenterManagement.Controllers
{
    public class StudentSide : Controller
    {

        #region Dependancey injuction

        private readonly ITeacherRepository _teacherRepository;
        private readonly IUserRepository _userRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IBarcodeRepository _barcodeRepository;
        private readonly ILessonRepository _lessonRepository;

        public StudentSide(ITeacherRepository teacherRepository,
                           IUserRepository userRepository,
                           IStudentRepository studentRepository,
                           IBarcodeRepository barcodeRepository,
                           ILessonRepository lessonRepository)
        {
            _teacherRepository = teacherRepository;
            _userRepository = userRepository;
            _studentRepository = studentRepository;
            _barcodeRepository = barcodeRepository;
            _lessonRepository = lessonRepository;
        }

        #endregion


        #region Subject Teachers

        [HttpGet]
        public async Task<IActionResult> SubjectTeachers(CategoryVM model)
        {
            ViewBag.teachers = await _teacherRepository.GetTeachersList();
            ViewBag.Category = model.Category;

            return View();
        }

        #endregion

        #region All Tracks

        [HttpGet]
        public async Task<IActionResult> AllTracks(Teacher model)
        {
            ViewBag.id = model.Id;

            var student = await _studentRepository.GetStudentData(await _userRepository.GitLoggingUserId());
            ViewBag.year = student.AcademyYear;

            return View();
        }

        #endregion

        #region Courses Track

        #region Choose Month

        [HttpGet]
        public async Task<IActionResult> ChooseMonth(TeacherIdVM model)
        {
            ViewBag.TeacherId = model.Id;
            ViewBag.Year = model.year;

            return View();
        }

        #endregion

        #region Enroll Barcode

        [HttpGet]
        public async Task<IActionResult> EnrollBarcode(Lesson model)
        {
            ViewBag.Month = model.Month;
            ViewBag.Year = model.AcademyYear;
            ViewBag.TeacherId = model.TeacherId;

            return View();
        }

        #endregion

        #region Lessons

        [HttpGet]
        public async Task<IActionResult> Lessons(MonthlyBarcodeVM model)
        {
            var result = await _barcodeRepository.CheckBarcode(model);

            if(result == true)
            {
                ViewBag.Year = model.AcademyYear;
                ViewBag.Month = model.Month;
                var teacher = await _teacherRepository.GetTeacherData(model.TeacherId);
                ViewBag.Teacher = teacher;

                ViewBag.Lessons = await _lessonRepository.GetTeacherLessonsList(model.TeacherId, model.AcademyYear, model.Month);

                return View();
            }
                        
            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region Lesson Details

        [HttpGet]
        public async Task<IActionResult> LessonDetails(Lesson model)
        {
            ViewBag.Lesson = await _lessonRepository.GetLessonDetails(model);

            return View();
        }

        #endregion

        #region Lesson Task

        [HttpGet]
        public async Task<IActionResult> LessonTask(Lesson model)
        {
            ViewBag.Tasks = await _lessonRepository.GetLessonTask(model.Id);
            ViewBag.LessonId = model.Id;

            return View();
        }

        #endregion

        #region Save Task

        [HttpPost]
        public async Task<IActionResult> SaveTask([FromForm] LessonTsakVM model)
        {
            var result = await _lessonRepository.UploadStudentTask(model);

            return RedirectToAction("Index", "Home");
        }

        #endregion

        #endregion

        #region Exams Track

        #endregion

    }
}
