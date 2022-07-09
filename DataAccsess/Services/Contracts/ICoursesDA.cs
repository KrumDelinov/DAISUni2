using DataAccsess.Entities;

namespace DataAccsess.Services.Contracts
{
    public interface ICoursesDA
    {
        IEnumerable<Course> GetCourses();

        Course GetCourseDetailsById(int id);

        Course GetCourseStudents(int id);

        Course GetCourseModules(int id);

        Course GetCourseMaterials(int id);

        Course GetEditCourseById(int id);

        Course GetCouraeById(int id);
    }
}