using CenterManagement.Models;
using CenterManagement.ViewModels;

namespace CenterManagement.IRepository
{
    public interface ITeacherRepository
    {

        public Task<Teacher> AddNewTeacher(FinishTDataVM model);

        public Task<Teacher> GetLoggingTeacherData();

        public Task<Teacher> UpdateTeacherData(UpdateTeacherDataVM model);

    }
}
