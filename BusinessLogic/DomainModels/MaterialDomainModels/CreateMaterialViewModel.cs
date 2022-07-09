
using BusinessLogic.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DomainModels.MaterialDomainModels
{
    public class CreateMaterialViewModel
    {
        public MaterialType MaterialType { get; set; }
        
        public string? MaterialUrl { get; set; }

        
        [Display(Name = "File")]
        public IFormFile? Content { get; set; }

        public int CoursesId { get; set; }
    }
}
