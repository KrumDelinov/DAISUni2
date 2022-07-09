using BusinessLogic.DomainModels.CourseDomainMoel;
using BusinessLogic.DomainModels.ModuleDomainModels;
using BusinessLogic.DomainModels.StudentDomainModel;
using DataAccsess.Entities;

namespace BusinessLogic.Services.Contracts
{
    public interface ICourseBL
    {
        ICollection<CourseViewModel> AllCourses();

        CreateCourseViewModel CreateCourseViewModel();

        Course CreateCourse(CreateCourseViewModel model);

        ICollection<StudentViewModel> StudentViewModels(int id);

        ICollection<ModuleViewModel> ModuleViewModels(int id);

        ICollection<CourseMaterialsViewModel> CourseMaterials(int id);

        public CourseDetailsViewModel Course(int id);

        CourseEditViewModel CourseEditViewMdel(int id);

        Task EditAsync(CourseEditViewModel viewModel);

        AddStudentsToCourseViewModel ViewModel(int id);

        void AddStudentToCourse(int courseId, int studentId);
    }
}