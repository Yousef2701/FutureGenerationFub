using CenterManagement.Models;
using CenterManagement.ViewModels;

namespace CenterManagement.IRepository
{
    public interface ITeacherRepository
    {

        public Task<Teacher> AddNewTeacher(FinishTDataVM model);

        public Task<Teacher> GetLoggingTeacherData();

        public Task<Teacher> UpdateTeacherData(UpdateTeacherDataVM model);

        public Task<int> GetTeachersCount();

        public Task<IEnumerable<Teacher>> GetTeachersList();

        public Task<Teacher> GetTeacherData(string teacherId);

        public Task<Teacher> DeleteTeacher(Teacher model);

    }
}
