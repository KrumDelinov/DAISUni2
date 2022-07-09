using BusinessLogic.DomainModels.MaterialDomainModels;
using BusinessLogic.Enums;
using BusinessLogic.Services.Contracts;
using DataAccsess.Entities;
using DataAccsess.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class MaterialBL : IMaterialBL
    {
        private readonly ICoursesDA coursesDA;
        private readonly IMaterialsDA materialsDA;

        public MaterialBL(ICoursesDA coursesDA, IMaterialsDA materialsDA)
        {
            this.coursesDA = coursesDA;
            this.materialsDA = materialsDA;
        }

        public ICollection<AllMaterialsViewModel> AllMaterials()
        {
            return materialsDA.AllMaterials().Select(m => new AllMaterialsViewModel
            {
                Id = m.Id,
                MaterialType = ((MaterialType)m.MaterialType).ToString(),
                MaterialUrl = m.MaterialUrl,
                CourseDetails = $"{m.Courses.Description}"
            }).ToList();
        }

        public ICollection<MaterialCoursesViewModel> Courses()
        {
            return coursesDA.GetCourses().Select(c => new MaterialCoursesViewModel
            {
                Id = c.Id,
                Name = c.Name,
            }).ToList();

        }

        public Material Crate(CreateMaterialViewModel model)
        {
            
            if (model.Content != null)
            {
                
                using (var memoryStream = new MemoryStream())
                {
                    model.Content.CopyTo(memoryStream);

                    var newMaterial = new Material
                    {
                        MaterialType = (int)model.MaterialType,
                        MaterialUrl = model.MaterialUrl,
                        CoursesId = model.CoursesId,
                        Content = memoryStream.ToArray()
                    };
                    return newMaterial;
                }

            }

            else
            {
                var newMaterial = new Material
                {
                    MaterialType = (int)model.MaterialType,
                    MaterialUrl = model.MaterialUrl,
                    CoursesId = model.CoursesId,
                };
                return newMaterial;
            }
        

        }

        public MaterialsDetailsViewModel MaterialViewModel(int id)
        {
            var mateial = materialsDA.GetMaterialById(id);

            var view = new MaterialsDetailsViewModel
            {
                Id = id,
                MaterialType = ((MaterialType)mateial.MaterialType).ToString(),
                MaterialUrl = mateial.MaterialUrl,
                CourseDetails = mateial.Courses.Name

            };

            return view;
        }
    }
}
