using CenterManagement.Controllers;
using CenterManagement.Models;
using CenterManagement.ViewModels;

namespace CenterManagement.IRepository
{
    public interface IStudentRepository
    {

        public Task<Student> AddNewStudent(FinishSDataVM model);

        public Task<int> GetStudentsCount();

        public Task<IEnumerable<Student>> GetStudentsList();

        public Task<Student> GetStudentData(string studentId);

        public Task<Student> UpdateStudentData(UpdateStudentDataVM model);

        public Task<Student> DeleteStudent(Student model);

        public Task<Student> DeleteStudent();

    }
}
