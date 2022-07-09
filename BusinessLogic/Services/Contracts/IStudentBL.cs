using BusinessLogic.DomainModels.StudentDomainModel;
using DataAccsess.Entities;

namespace BusinessLogic.Services.Contracts
{
    public interface IStudentBL
    {
        ICollection<StudentViewModel> GetAllNoDeletedStudnts();

        Student CreateStudent (CreateStudentViewModel student);

        Task<EditStudentViewModel> EditdByIdAsync(int id);

        Task<DetailsStudentViewModel> DetailsStudentByIdAsync(int id);

        Student UpdateStudent(EditStudentViewModel student);
    }
}