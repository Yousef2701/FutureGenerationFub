using System.Security.Claims;
using ArabityAuth;
using CenterManagement.IRepository;
using CenterManagement.Models;
using CenterManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace CenterManagement.Controllers
{
    public class TeacherManagement : Controller
    {

        #region Dependancey injuction

        private readonly IExamRepository _examRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly ILessonRepository _lessonRepository;
        private readonly IBarcodeRepository _barcodeRepository;

        public TeacherManagement(IExamRepository examRepository,
                                 ITeacherRepository teacherRepository,
                                 ILessonRepository lessonRepository,
                                 IBarcodeRepository barcodeRepository)
        {
            _examRepository = examRepository;
            _teacherRepository = teacherRepository;
            _lessonRepository = lessonRepository;
            _barcodeRepository = barcodeRepository;
        }

        #endregion


        #region Comlete Teacher Data

        [HttpGet]
        public async Task<IActionResult> ComleteTeacherData()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ComleteTeacherData([FromForm] FinishTDataVM model)
        {
            var result = await _teacherRepository.AddNewTeacher(model);

            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region Dashboard

        public async Task<IActionResult> Dashboard()
        {
            return View();
        }

        #endregion

        #region Add New Lesson

        [HttpGet]
        public async Task<IActionResult> AddNewLesson()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewLesson([FromForm] LessonVM model)
        {
           var result = await _lessonRepository.AddNewLesson(model);

            return RedirectToAction("AddLessonTask");
        }

        #endregion

        #region Add Lesson Task

        [HttpGet]
        public async Task<IActionResult> AddLessonTask(Lesson model)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddLessonTask(LessonTask model)
        {
            var result = await _lessonRepository.AddLessonTask(model);

            return View();
        }

        #endregion

        #region Add Exam

        [HttpGet]
        public async Task<IActionResult> AddExam()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddExam(Exam model)
        {
            var result = await _examRepository.CreateNewExam(model);

            return RedirectToAction("AddQuestion");
        }

        #endregion

        #region Add Question

        [HttpGet]
        public async Task<IActionResult> AddQuestion()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddQuestion(Question model)
        {
            var result = await _examRepository.CreateNewQuestion(model);

            return View();
        }

        #endregion

        #region Teacher Barcodes List

        public async Task<IActionResult> TeacherBarcodesList()
        {
            return View();
        }

        #endregion

        #region Choose Month

        public async Task<IActionResult> ChooseMonth(AcademYearVM model)
        {
            ViewBag.year = model.AcademyYear;

            return View();
        }

        #endregion

        #region Get Barcodes

        public async Task<IActionResult> GetBarcodes(AcademYearVM model)
        {  
            ViewBag.Barcodes = await _barcodeRepository.GetBarcodes(model);

            return View();
        }

        #endregion

        #region Teacher Lessons List

        public async Task<IActionResult> TeacherLessonsList()
        {   
            ViewBag.Lessons = await _lessonRepository.GetTeacherLessonsList();

            return View();
        }

        #endregion

        #region Students Task List

        public async Task<IActionResult> StudentsTaskList(Lesson model)
        {
            ViewBag.Tasks = await _lessonRepository.GetStudentsTaskList(model);

            return View();
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

        #region Update Lesson Details

        [HttpGet]
        public async Task<IActionResult> UpdateLessonDetails(Lesson model)
        {
            ViewBag.Lesson = await _lessonRepository.GetLessonDetails(model);

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateLessonDetails(LessonVM model)
        {
            var result = await _lessonRepository.UpdateLessonDetails(model);

            return RedirectToAction("TeacherLessonsList");
        }

        #endregion

        #region Teacher Exams List

        [HttpGet]
        public async Task<IActionResult> TeacherExamsList()
        {
            ViewBag.Exams = await _examRepository.GetTeacherExamsList();

            return View();
        }

        #endregion

        #region Exam Students Result List

        [HttpGet]
        public async Task<IActionResult> ExamStudentsResultList(Exam model)
        {
            ViewBag.Results = await _examRepository.GetExamStudentsResultList(model.Id);
            ViewBag.Count = await _examRepository.GetExamQuestionsCount(model.Id);

            return View();
        }

        #endregion

        #region Exam Questions List

        [HttpGet]
        public async Task<IActionResult> ExamQuestionsList(Exam model)
        {
            ViewBag.Questions = await _examRepository.GetExamQuestionsList(model.Id);

            return View();
        }

        #endregion       

        #region Update Question

        [HttpGet]
        public async Task<IActionResult> UpdateQuestion(Question model)
        {
            ViewBag.Question = await _examRepository.GetQuestionData(model);

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> UpdateQuestion(QuestionVM model)
        {
            var result = await _examRepository.UpdateQuestionData(model);

            return RedirectToAction("TeacherExamsList");
        }

        #endregion

        #region Remove Question

        public async Task<IActionResult> RemoveQuestion(Question model)
        {
            var result = await _examRepository.RemoveQuestion(model);

            return RedirectToAction("TeacherExamsList");
        }

        #endregion

        #region Delete Exam

        public async Task<IActionResult> DeleteExam(Exam model)
        {
            var result = await _examRepository.DeleteExam(model.Id);

            return RedirectToAction("TeacherExamsList");
        }

        #endregion

        #region Personal Page

        public async Task<IActionResult> PersonalPage()
        {
            ViewBag.Teacher = await _teacherRepository.GetLoggingTeacherData();

            return View();
        }

        #endregion

        #region Update Teacher Data

        [HttpGet]
        public async Task<IActionResult> UpdateTeacherData()
        {
            ViewBag.Teacher = await _teacherRepository.GetLoggingTeacherData();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTeacherData([FromForm] UpdateTeacherDataVM model)
        {
            var result = await _teacherRepository.UpdateTeacherData(model);

            return RedirectToAction("PersonalPage");
        }

        #endregion 

    }
}
