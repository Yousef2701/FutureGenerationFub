using System.Security.Claims;
using ArabityAuth;
using CenterManagement.Data;
using CenterManagement.IRepository;
using CenterManagement.Models;
using CenterManagement.ViewModels;

namespace CenterManagement.Repository
{
    public class TeacherRepository : ITeacherRepository
    {

        #region Dependancey injuction

        private readonly ApplicationDbContext _context;
        private readonly IUserRepository _userRepository;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _Environment;

        public TeacherRepository(ApplicationDbContext context,
                                 IUserRepository userRepository,
                                 Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _context = context;
            _userRepository = userRepository;
            _Environment = environment;
        }

        #endregion


        #region Add New Teacher

        public async Task<Teacher> AddNewTeacher(FinishTDataVM model)
        {
            if(model != null)
            {
                var image = new Tools(_Environment);
                string imageUrl = image.AddImages(model.ImageFile, model.Username);

                string id = _context.Users.Where(m => m.Email == model.Username).Select(m => m.Id).FirstOrDefault();
                var teacher = new Teacher
                {
                    Id = id,
                    FristName = model.FristName,
                    LastName = model.LastName,
                    Subject = model.Subject,
                    PhoneNumber = model.PhoneNumber,
                    Email = model.Email,
                    ImageUrl = imageUrl,
                    Governorate = model.Governorate,
                    City = model.City
                };

                _context.teachers.Add(teacher);
                _context.SaveChanges();

                return teacher;
            }
            return null;
        }

        #endregion

        #region Get Logging Teacher Data

        public async Task<Teacher> GetLoggingTeacherData()
        {
            var userId = await _userRepository.GitLoggingUserId();

            var teacher = _context.teachers.Where(m => m.Id == userId).FirstOrDefault();
            return teacher;
        }

        #endregion

        #region Update Teacher Data

        public async Task<Teacher> UpdateTeacherData(UpdateTeacherDataVM model)
        {
            if(model != null)
            {
                var userId = await _userRepository.GitLoggingUserId();
                var CurrentTeacher = _context.teachers.Where(m => m.Id == userId).FirstOrDefault();

                if(model.ImageFile != null)
                {
                    var username = _context.Users.Where(m => m.Id == userId).Select(o => o.UserName).FirstOrDefault();
                    var image = new Tools(_Environment);
                    string imageUrl = image.AddImages(model.ImageFile, username);

                    CurrentTeacher.ImageUrl = imageUrl;
                }
                else
                {
                    CurrentTeacher.ImageUrl = CurrentTeacher.ImageUrl;
                }
                

                CurrentTeacher.Id = userId;
                CurrentTeacher.FristName = model.FristName;
                CurrentTeacher.LastName = model.LastName;
                CurrentTeacher.Subject = model.Subject;
                CurrentTeacher.PhoneNumber = model.PhoneNumber;
                CurrentTeacher.Email = model.Email;
                CurrentTeacher.Governorate = model.Governorate;
                CurrentTeacher.City = model.City;

                _context.teachers.Update(CurrentTeacher);
                _context.SaveChanges();

                return CurrentTeacher;
            }
            return null;
        }

        #endregion

    }
}
