using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DomainModels.ModuleDomainModels
{
    public class AllModuleViewModel
    {
        public ICollection<ModuleViewModel> Modules { get; set; }
    }
}
