using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DomainModels.CourseDomainMoel
{
    public class CourseDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public int Difficulty { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public string TeacherName { get; set; }

        public int SdudentsCount  { get; set; }

        public int ModulesCount { get; set; }

        public int MaterialsCount { get; set; }
    }
}
