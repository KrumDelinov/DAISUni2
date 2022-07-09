using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DomainModels.MaterialDomainModels
{
    public class BufferedSingleFileUploadDbModel 
    {
        [BindProperty]
        public CreateMaterialViewModel FileUpload { get; set; }
    }
}
