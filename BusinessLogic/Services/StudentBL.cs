using BusinessLogic.DomainModels.StudentDomainModel;
using BusinessLogic.Services.Contracts;
using DataAccsess.Entities;
using DataAccsess.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class StudentBL : IStudentBL
    {
        private readonly IStudentsDA studentsDA;

        public StudentBL(IStudentsDA studentsDA)
        {
            this.studentsDA = studentsDA;
        }

        public Student CreateStudent(CreateStudentViewModel student)
        {
            return new Student 
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                BirthDate = student.BirthDate,
                CreatedOn = DateTime.Now,
            };
        }

        public async Task<DetailsStudentViewModel> DetailsStudentByIdAsync(int id)
        {
            var student = await studentsDA.FindByIdAsunc(id);
            return new DetailsStudentViewModel
            {
                Id = id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                BirthDate = student.BirthDate,
                CreatedOn = student.CreatedOn,
                LastUpdatedOn = student.LastUpdatedOn,
                IsDeleted = student.IsDeleted,
                DeletedOn = student.DeletedOn,

            };
        }

        public async Task<EditStudentViewModel> EditdByIdAsync(int id)
        {
            var student = await studentsDA.FindByIdAsunc(id);
            return new EditStudentViewModel
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                BirthDate = student.BirthDate,
                CreatedOn = student.CreatedOn,
                
            };
        }

        public ICollection<StudentViewModel> GetAllNoDeletedStudnts()
        {
            return studentsDA.GetAllNoDeletedStudnts().Select(s => new StudentViewModel
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Email = s.Email,
                BirthDate = s.BirthDate,

            }).ToList();
        }

        public Student UpdateStudent(EditStudentViewModel editStudent)
        {
            var student = studentsDA.FindById(editStudent.Id);

            student.FirstName = editStudent.FirstName;
            student.LastName = editStudent.LastName;
            student.Email = editStudent.Email;
            student.BirthDate = editStudent.BirthDate;
            student.CreatedOn = editStudent.CreatedOn;
            student.LastUpdatedOn = DateTime.Now;
            return student;
        }
    }
}
