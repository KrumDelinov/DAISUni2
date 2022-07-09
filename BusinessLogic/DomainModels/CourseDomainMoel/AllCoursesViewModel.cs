using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DomainModels.CourseDomainMoel
{
    public class AllCoursesViewModel
    {
        public IEnumerable<CourseViewModel> Courses { get; set; }
    }
}
