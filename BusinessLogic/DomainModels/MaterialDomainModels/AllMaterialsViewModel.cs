using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DomainModels.MaterialDomainModels
{
    public class AllMaterialsViewModel
    {
        public int Id { get; set; }

        public string MaterialType { get; set; }

        public string MaterialUrl { get; set; }

        public string CourseDetails { get; set; }
    }
}
