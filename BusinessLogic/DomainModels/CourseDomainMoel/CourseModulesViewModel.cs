using BusinessLogic.DomainModels.ModuleDomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DomainModels.CourseDomainMoel
{
    public class CourseModulesViewModel
    {
        public ICollection<ModuleViewModel> Modules { get; set; }
    }
}
