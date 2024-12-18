using CenterManagement.Controllers;
using CenterManagement.Data;
using CenterManagement.IRepository;
using CenterManagement.Models;

namespace CenterManagement.Repository
{
    public class StudentRepository : IStudentRepository
    {

        #region Dependancey injuction

        private readonly ApplicationDbContext _context;
        private readonly IUserRepository _userRepository;

        public StudentRepository(ApplicationDbContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
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

        #region Delete Student

        public async Task<Student> DeleteStudent(Student model)
        {
            if(model != null)
            {
                var student = _context.student.Find(model.Id);

                _context.student.Remove(student);

                var role = _context.UserClaims.Where(m => m.UserId == model.Id).FirstOrDefault();
                _context.UserClaims.Remove(role);

                var user = _context.Users.Where(m => m.Id == model.Id).FirstOrDefault();
                _context.Users.Remove(user);

                _context.SaveChanges();

                return student;
            }
            return null;
        }

        #endregion

    }
}
