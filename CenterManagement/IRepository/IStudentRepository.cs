using CenterManagement.Controllers;
using CenterManagement.Models;

namespace CenterManagement.IRepository
{
    public interface IStudentRepository
    {

        public Task<int> GetStudentsCount();

        public Task<IEnumerable<Student>> GetStudentsList();

        public Task<Student> GetStudentData(string studentId);

        public Task<Student> DeleteStudent(Student model);

    }
}
