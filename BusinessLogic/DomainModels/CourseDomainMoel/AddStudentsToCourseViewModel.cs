using DataAccsess.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DomainModels.CourseDomainMoel
{
    public class AddStudentsToCourseViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int StudentId { get; set; }

        public ICollection<AddStudentsModel>? FreeStudents { get; set; }
    }
}
