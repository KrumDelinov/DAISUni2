using BusinessLogic.DomainModels.TeacherDomainModels;
using BusinessLogic.Services.Contracts;
using DataAccsess.Entities;
using DataAccsess.Services.Contracts;

namespace BusinessLogic.Services
{
    public class TeacherBL : ITeacherBL
    {
        private readonly ITeachersDA teachersDA;

        public TeacherBL(ITeachersDA teachersDA)
        {
            this.teachersDA = teachersDA;
        }
        public IEnumerable<TeacherViewModel> GetTeachers(int maxCount = 100)
        {
            return teachersDA
                .GetTachers(maxCount).Select(Map);

        }
        public async Task<Teacher> FindTeacherByIdAsynk(int id)
        {
            var teacher = await teachersDA.FindTeacherByIdAsynk(id);
            return teacher;
        }

        public TeacherViewModel GetTeacherById(int id)
        {
            var teacher = teachersDA.GetTeacherById(id);

            return Map(teacher);
        }

        public void UpdateTeacher(Teacher teacher)
        {
            teacher.LastUpdatedOn = DateTime.Now;
            teachersDA.UpdateTeacher(teacher);

        }

        public async Task SaveChagesAsync()
        {
            await teachersDA.SaveChagesAsync();
        }

        public bool TeacherExists(int id)
        {
            return teachersDA.TeacherExists(id);
        }

        public TeacherViewModel Map(Teacher teacher)
        {
            return new TeacherViewModel
            {
                Id = teacher.Id,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                HiredOn = (DateTime)teacher.HiredOn,
                Email = teacher.Email,
                CreatedOn = teacher.CreatedOn,
            };
        }

        public Teacher CreateTeacher(CreateTeacherViewModel viewModel)
        {
            var teacher = new Teacher
            {
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Email = viewModel.Email,
                HiredOn = viewModel.HiredOn,
                CreatedOn = DateTime.Now,
            };
            return teacher;
        }

        public void AddTeacher(Teacher teacher)
        {
            teachersDA.AddTeacher(teacher);
        }
    }
}