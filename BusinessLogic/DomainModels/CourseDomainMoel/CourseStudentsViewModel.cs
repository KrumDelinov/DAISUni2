using BusinessLogic.DomainModels.StudentDomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DomainModels.CourseDomainMoel
{
    public class CourseStudentsViewModel
    {
        public ICollection<StudentViewModel> Students { get; set; }
    }
}
