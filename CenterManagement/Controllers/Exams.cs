using CenterManagement.Data;
using CenterManagement.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using CenterManagement.ViewModels;
using CenterManagement.IRepository;

namespace CenterManagement.Controllers
{
    public class Exams : Controller
    {

        #region Dependancey injuction

        private readonly IExamRepository _examRepository;

        public Exams(IExamRepository examRepository)
        {
            _examRepository = examRepository;
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

        #region Teacher Exams List

        [HttpGet]
        public async Task<IActionResult> TeacherExamsList()
        {
            ViewBag.Exams = await _examRepository.GetTeacherExamsList();

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

        #region Exam Students Result List

        [HttpGet]
        public async Task<IActionResult> ExamStudentsResultList(Exam model)
        {
            ViewBag.Results = await _examRepository.GetExamStudentsResultList(model.Id);
            ViewBag.Count = await _examRepository.GetExamQuestionsCount(model.Id);

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

    }
}
