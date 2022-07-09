using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DomainModels.TeacherDomainModels
{
    public class AllTeachersViewModel
    {
        public IEnumerable<TeacherViewModel> Teachers { get; set; }
    }
}
