using BusinessLogic.DomainModels.ModuleDomainModels;
using DataAccsess.Entities;

namespace BusinessLogic.Services.Contracts
{
    public interface IModuleBL
    {
        CreateModuleViewModel CreateModuleViewModel();
        Module CreateModule(CreateModuleViewModel viewModel);
        ICollection<ModuleViewModel> AllModules();
        ModuleDetailsViewModel Details(int id);
        ModuleEditViewModel EditModuleViewModel(int id);

        Module Edit(ModuleEditViewModel viewModel);
    }
}