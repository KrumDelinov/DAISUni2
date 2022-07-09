using BusinessLogic.DomainModels.ModuleDomainModels;
using BusinessLogic.Services.Contracts;
using DataAccsess.Entities;
using DataAccsess.Services.Contracts;
using EfRepsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class ModuleBL : IModuleBL
    {
        private readonly ICoursesDA coursesDA;
        private readonly IModuleDA moduleDA;
        

        public ModuleBL(ICoursesDA coursesDA, IModuleDA moduleDA)
        {
            this.coursesDA = coursesDA;
            this.moduleDA = moduleDA;
        }
        public CreateModuleViewModel CreateModuleViewModel()
        {
           var courses = CreateModuleCoursesView();

            return new CreateModuleViewModel
            {
                Courses = courses
            };
        }

        

        public Module CreateModule(CreateModuleViewModel viewModel)
        {
            return new Module
            {
                Name = viewModel.Name,
                Description = viewModel.Description,
                CoursesId = viewModel.CoursesId,
                CreatedOn = DateTime.Now,
            };
        }

        public ICollection<ModuleViewModel> AllModules()
        {
            return moduleDA.AllModules().Select(m => new ModuleViewModel 
            { 
                Id = m.Id,
                Name= m.Name,
                Description= m.Description,
                CourseName = m.Courses.Name,
            }).ToList();
        }

        public ModuleDetailsViewModel Details(int id)
        {
            var module = moduleDA.GetModuleById(id);
            return new ModuleDetailsViewModel 
            {
                Id = id, 
                Name = module.Name,
                Description = module.Description,
                CourseName = module.Courses.Name,
                CreatedOn= module.CreatedOn,
            };
        }

        public ModuleEditViewModel EditModuleViewModel(int id)
        {
            var module = moduleDA.GetModuleById(id);
            var courses = CreateModuleCoursesView();
            return new ModuleEditViewModel
            {
                Id = module.Id,
                Name = module.Name,
                Description = module.Description,
                CoursesId= module.Courses.Id,
                Courses = courses,
            };
        }

        public Module Edit(ModuleEditViewModel viewModel)
        {
            var module = moduleDA.GetModuleById(viewModel.Id);

            module.Name = viewModel.Name;
            module.Description = viewModel.Description;
            module.CoursesId = viewModel.CoursesId;
            module.LastUpdatedOn = DateTime.Now;
            module.CreatedOn = module.CreatedOn;
            return module;
        }

      

        private List<ModuleCoursesViewModel> CreateModuleCoursesView()
        {
            return coursesDA.GetCourses().Select(c => new ModuleCoursesViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
            }).ToList();
        }
    }
}
