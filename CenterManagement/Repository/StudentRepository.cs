using System.Security.Claims;
using ArabityAuth;
using CenterManagement.Controllers;
using CenterManagement.Data;
using CenterManagement.IRepository;
using CenterManagement.Models;
using CenterManagement.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace CenterManagement.Repository
{
    public class StudentRepository : IStudentRepository
    {

        #region Dependancey injuction

        private readonly ApplicationDbContext _context;
        private readonly IUserRepository _userRepository;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _Environment;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<HomeController> _logger;

        public StudentRepository(ApplicationDbContext context,
                                 IUserRepository userRepository,
                                 Microsoft.AspNetCore.Hosting.IHostingEnvironment environment,
                                 SignInManager<IdentityUser> signInManager,
                                 ILogger<HomeController> logger)
        {
            _context = context;
            _userRepository = userRepository;
            _Environment = environment;
            _signInManager = signInManager;
            _logger = logger;
        }

        #endregion


        #region Add New Student

        public async Task<Student> AddNewStudent(FinishSDataVM model)
        {
            if (model != null)
            {
                var image = new Tools(_Environment);
                string imageUrl = image.AddImages(model.ImageFile, model.Username);

                string id = _context.Users.Where(m => m.Email == model.Username).Select(m => m.Id).FirstOrDefault();
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

                return student;
            }
            return null;
        }

        #endregion

        #region Get Students Count

        public async Task<int> GetStudentsCount()
        {
            return _context.student.Count();
        }

        #endregion

        #region Get Students List

        public async Task<IEnumerable<Student>> GetStudentsList()
        {
            return _context.student.OrderBy(m => m.FristName).ThenBy(m => m.LastName).ToList();
        }

        #endregion

        #region Get Student Data

        public async Task<Student> GetStudentData(string studentId)
        {
            if(studentId != null)
            {
                return _context.student.Find(studentId);
            }
            return null;
        }

        #endregion

        #region Update Student Data

        public async Task<Student> UpdateStudentData(UpdateStudentDataVM model)
        {
            if (model != null)
            {
                var userId = await _userRepository.GitLoggingUserId();
                var CurrentStudent = _context.student.Where(m => m.Id == userId).FirstOrDefault();

                if(model.ImageFile != null)
                {
                    var username = _context.Users.Where(m => m.Id == userId).Select(o => o.UserName).FirstOrDefault();
                    var image = new Tools(_Environment);
                    string imageUrl = image.AddImages(model.ImageFile, username);

                    CurrentStudent.ImageUrl = imageUrl;
                }
                else
                {
                    CurrentStudent.ImageUrl = CurrentStudent.ImageUrl;
                }
               

                CurrentStudent.Id = userId;
                CurrentStudent.FristName = model.FristName;
                CurrentStudent.LastName = model.LastName;
                CurrentStudent.PhoneNumber = model.PhoneNumber;
                CurrentStudent.GuardingPhoneNumber = model.GuardingPhoneNumber;
                CurrentStudent.Email = model.Email;
                CurrentStudent.Governorate = model.Governorate;
                CurrentStudent.City = model.City;
                CurrentStudent.AcademyYear = model.AcademyYear;

                _context.student.Update(CurrentStudent);
                _context.SaveChanges();

                return CurrentStudent;
            }
            return null;
        }

        #endregion

        #region Delete Student

        public async Task<Student> DeleteStudent(Student model)
        {
            if (model != null)
            {
                await _signInManager.SignOutAsync();
                _logger.LogInformation("User logged out.");

                var student = _context.student.Find(model.Id);
                _context.student.Remove(student);

                var role = _context.UserClaims.Where(m => m.UserId == model.Id).FirstOrDefault();
                _context.UserClaims.Remove(role);

                var user = _context.Users.Find(model.Id);
                _context.Users.Remove(user);

                _context.SaveChanges();

                return student;
            }

            return null;
        }

        public async Task<Student> DeleteStudent()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            var userId = await _userRepository.GitLoggingUserId();

            if (userId != null)
            {
                var student = _context.student.Find(userId);
                _context.student.Remove(student);

                var role = _context.UserClaims.Where(m => m.UserId == userId).FirstOrDefault();
                _context.UserClaims.Remove(role);

                var user = _context.Users.Find(userId);
                _context.Users.Remove(user);

                _context.SaveChanges();

                return student;
            }

            return null;
        }

        #endregion

    }
}
