using DataAccsess.Entities;

namespace DataAccsess.Services.Contracts
{
    public interface IMaterialsWithADO
    {
        Material GetMaterialById(int id);
        string GetSelectMaterialaById();
    }
}