using BusinessLogic.DomainModels.CourseDomainMoel;
using BusinessLogic.DomainModels.ModuleDomainModels;
using BusinessLogic.DomainModels.StudentDomainModel;
using BusinessLogic.Enums;
using BusinessLogic.Services.Contracts;
using DataAccsess.Entities;
using DataAccsess.Services.Contracts;
using EfRepsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class CourseBL : ICourseBL
    {
        private readonly ICoursesDA coursesDA;
        private readonly ITeachersDA teachersDA;
        private readonly IStudentsDA studentsDA;
        private readonly IStudentsWithADO studentsWithADO;
        private readonly IEntityRepository entityRepository;

        public CourseBL(ICoursesDA coursesDA,
            ITeachersDA teachersDA,
            IStudentsDA studentsDA, 
            IStudentsWithADO studentsWithADO,
            IEntityRepository entityRepository)
        {
            this.coursesDA = coursesDA;
            this.teachersDA = teachersDA;
            this.studentsDA = studentsDA;
            this.studentsWithADO = studentsWithADO;
            this.entityRepository = entityRepository;
        }
        public ICollection<CourseViewModel> AllCourses()
        {
            var viewModel = new AllCoursesViewModel();

            var coursesView = coursesDA.GetCourses().Select(c => new CourseViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Difficulty = c.Difficulty,
                StidentCount = c.StudentsCourses.Count,
                VotesCount = c.Votes.Count,
                StartDate = c.StartDate,
                EndDate = c.EndDate,
                
            }).ToList();
            foreach (var item in coursesView)
            {
                var isRunning = true;
                var dateNow = DateTime.Now;
                if (item.StartDate <= dateNow && item.EndDate >= dateNow )
                {
                    item.IsRunning = "True";
                }
                else
                {
                    item.IsRunning = "False";
                }

            }

            return coursesView;
        }

        public Course CreateCourse(CreateCourseViewModel model)
        {
            var newCourse = new Course
            {
                Name = model.Name,
                Description = model.Description,
                Difficulty = model.Difficulty,
                TeachersId = model.TeachersId,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                CreatedOn = DateTime.Now,
            };
            return newCourse;
        }

        public CreateCourseViewModel CreateCourseViewModel()
        {
            var courseTeachers = teachersDA.GetTachers(200).Select(t => new CourseTeachersViewModel
            {
                Id = t.Id,
                FirstName = t.FirstName,
                LastName = t.LastName,
                Email = t.Email,
            }
            ).ToList();
            var courseView = new CreateCourseViewModel
            {
                Teachers = courseTeachers
            };
            return courseView;
        }

        public ICollection<ModuleViewModel> ModuleViewModels(int id)
        {
            var course = coursesDA.GetCourseModules(id);
            return course.Modules
                .Select(m => new ModuleViewModel
                {
                    Id = m.Id,
                    Name = m.Name,
                    Description = m.Description,
                    CourseName = course.Name,
                }).ToList();

        }

        public ICollection<StudentViewModel> StudentViewModels(int id)
        {
            var courseStudnts = coursesDA.GetCourseStudents(id).StudentsCourses;

            List<StudentViewModel> studentViewModels = new List<StudentViewModel>();

            foreach (var student in courseStudnts)
            {
                var courseStudent = studentsDA.FindById(student.StudentsId);
                var viewModel = new StudentViewModel
                {
                    Id = courseStudent.Id,
                    FirstName = courseStudent.FirstName,
                    LastName = courseStudent.LastName,
                    BirthDate = courseStudent.BirthDate,
                    Email = courseStudent.Email,
                };
                studentViewModels.Add(viewModel);
            }

            return studentViewModels;
        }

        public ICollection<CourseMaterialsViewModel> CourseMaterials(int id)
        {
            return coursesDA.GetCourseMaterials(id).Materials
                .Select(m => new CourseMaterialsViewModel
                {
                    Id = m.Id,
                    MaterialType = ((MaterialType)m.MaterialType).ToString(),
                    MaterialUrl = m.MaterialUrl,
                }).ToList();
        }

        public CourseDetailsViewModel Course(int id)
        {
            var course = coursesDA.GetCourseDetailsById(id);

            return new CourseDetailsViewModel
            {
                Id = course.Id,
                Description = course.Description,
                Difficulty = course.Difficulty,
                StartDate = course.StartDate,
                EndDate = course.EndDate,
                Name = course.Name,
                TeacherName = course.Teachers.FirstName + " " + course.Teachers.LastName,
                SdudentsCount = course.StudentsCourses.Count,
                ModulesCount = course.Modules.Count,
                MaterialsCount = course.Materials.Count,
            };
        }

        public CourseEditViewModel CourseEditViewMdel (int id)
        {
            
            var courseTeachers = teachersDA.GetTachers(200).Select(t => new CourseTeachersViewModel
            {
                Id = t.Id,
                FirstName = t.FirstName,
                LastName = t.LastName,
                Email = t.Email,
            }
          ).ToList();

            var course = coursesDA.GetEditCourseById(id);
         
         
            var view = new CourseEditViewModel
            {
                Id=course.Id,
                Name = course.Name,
                Description = course.Description,
                Difficulty=course.Difficulty,
                StartDate=course.StartDate,
                EndDate=course.EndDate,
                TeachersId = course.TeachersId,
                Teachers = courseTeachers.ToList(),
            };
            
            var dateNow = DateTime.Now;
            if (course.StartDate <= dateNow && course.EndDate >= dateNow)
            {
                view.IsRunning = true;
            }
            else
            {
                view.IsRunning = false;
            }
            return view;
        }

        public async Task EditAsync(CourseEditViewModel viewModel)
        {
            var course = coursesDA.GetCouraeById(viewModel.Id);

            course.Difficulty = viewModel.Difficulty;
            course.StartDate = viewModel.StartDate;
            course.EndDate = viewModel.EndDate;
            course.Name = viewModel.Name;
            course.Description = viewModel.Description;
            course.LastUpdatedOn = DateTime.Now;
            course.TeachersId = viewModel.TeachersId;

            entityRepository.Update(course);
            await entityRepository.SaveChangesAsync();
        }

        public AddStudentsToCourseViewModel ViewModel(int id)
        {
            var course = coursesDA.GetCouraeById(id);
            var students = studentsWithADO.AddStudentsModels(course.Id).ToList();

            var view = new AddStudentsToCourseViewModel
            {
                Id = course.Id,
                Name = course.Name,
                FreeStudents = students
            };

            return view;
        }

        public void AddStudentToCourse(int courseId, int studentId)
        {
            var course = coursesDA.GetCouraeById(courseId); 
            var studentCoures = new StudentsCourse { CoursesId = courseId, StudentsId = studentId };
            course.StudentsCourses.Add(studentCoures);
            entityRepository.Add(studentCoures);
            entityRepository.SaveChangesAsync();
        }
    }
}
