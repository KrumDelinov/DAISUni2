using BusinessLogic.DomainModels.MaterialDomainModels;
using DataAccsess.Entities;

namespace BusinessLogic.Services.Contracts
{
    public interface IMaterialBL
    {
        ICollection<MaterialCoursesViewModel> Courses();
        Material Crate(CreateMaterialViewModel model);

        ICollection<AllMaterialsViewModel> AllMaterials();

        MaterialsDetailsViewModel MaterialViewModel(int id);
    }
}