using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DomainModels.CourseDomainMoel
{
    public class CreateCourseViewModel
    {
        
        [Required]
        public string Name { get; set; }
        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        [Range(1, 5,ErrorMessage = "Value for Difficulty must be between {1} and {2}")]
        public int Difficulty { get; set; }
        public int? TeachersId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public ICollection<CourseTeachersViewModel>? Teachers { get; set; }
    }
}
