using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DomainModels.CourseDomainMoel
{
    public class CourseViewModel 
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
       
        public string Description { get; set; }

        public int Difficulty { get; set; }

        public int StidentCount { get; set; }

        public int VotesCount { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string IsRunning { get; set; }

       
    }
}
