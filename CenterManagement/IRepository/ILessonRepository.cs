using CenterManagement.Models;
using CenterManagement.ViewModels;

namespace CenterManagement.IRepository
{
    public interface ILessonRepository
    {

        public Task<Lesson> AddNewLesson(LessonVM model);

        public Task<LessonTask> AddLessonTask(LessonTask model);

        public Task<IEnumerable<Lesson>> GetTeacherLessonsList();

        public Task<IEnumerable<Lesson>> GetTeacherLessonsList(string teacherId, string year, string month);

        public Task<IEnumerable<LessonTask>> GetLessonTask(string lessinId);

        public Task<IEnumerable<Students_Task>> GetStudentsTaskList(Lesson model);

        public Task<string> UploadStudentTask(LessonTsakVM model);

        public Task<Lesson> GetLessonDetails(Lesson model);

        public Task<Lesson> UpdateLessonDetails(LessonVM model);

    }
}
