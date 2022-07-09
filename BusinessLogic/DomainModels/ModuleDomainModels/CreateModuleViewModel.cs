using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DomainModels.ModuleDomainModels
{
    public class CreateModuleViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public int? CoursesId { get; set; }

        public ICollection<ModuleCoursesViewModel>? Courses { get; set; }
    }
}
