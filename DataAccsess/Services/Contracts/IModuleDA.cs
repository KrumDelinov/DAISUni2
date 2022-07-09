using DataAccsess.Entities;

namespace DataAccsess.Services.Contracts
{
    public interface IModuleDA
    {
        ICollection<Module> AllModules();
        Module GetModuleById(int id);
    }
}