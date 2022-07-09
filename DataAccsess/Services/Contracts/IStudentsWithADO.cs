using DataAccsess.Entities;
using DataAccsess.EntityModels;

namespace DataAccsess.Services.Contracts
{
    public interface IStudentsWithADO
    {
        IEnumerable<AddStudentsModel> AddStudentsModels(int courseId);

        IEnumerable<StudentsCourse> StudentsCourses();
    }
}