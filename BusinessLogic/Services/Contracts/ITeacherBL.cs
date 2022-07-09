using BusinessLogic.DomainModels.TeacherDomainModels;
using DataAccsess.Entities;

namespace BusinessLogic.Services.Contracts
{
    public interface ITeacherBL
    {
        Task<Teacher> FindTeacherByIdAsynk(int id);
        TeacherViewModel GetTeacherById(int id);
        IEnumerable<TeacherViewModel> GetTeachers(int maxCount = 100);
        TeacherViewModel Map(Teacher teacher);
        Task SaveChagesAsync();
        bool TeacherExists(int id);
        void UpdateTeacher(Teacher teacher);
        Teacher CreateTeacher(CreateTeacherViewModel viewModel);
        void AddTeacher(Teacher teacher);
    }
}