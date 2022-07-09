using DataAccsess.Entities;

namespace DataAccsess.Services.Contracts
{
    public interface ITeachersDA
    {
        Task<Teacher> FindTeacherByIdAsynk(int id);
        IEnumerable<Teacher> GetTachers(int maxCount);
        Teacher GetTeacherById(int id);
        Task SaveChagesAsync();
        bool TeacherExists(int id);
        void UpdateTeacher(Teacher teacher);
        void AddTeacher(Teacher teacher);
    }
}