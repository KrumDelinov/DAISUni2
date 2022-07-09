using DataAccsess.Entities;

namespace DataAccsess.Services.Contracts
{
    public interface IMaterialsDA
    {
        ICollection<Material> AllMaterials();

        Material GetMaterialById(int id);
    }
}