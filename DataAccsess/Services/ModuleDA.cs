using DataAccsess.DbContexts;
using DataAccsess.Entities;
using DataAccsess.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccsess.Services
{
    public class ModuleDA : IModuleDA
    {
        private DAISUni2Context dbContext;

        public ModuleDA(DAISUni2Context dbContext, IConfiguration configuration)
        {
            this.dbContext = dbContext;
        }



        public ICollection<Module> AllModules()
        {
            return dbContext.Modules.Include(c => c.Courses).ToList();
        }

        public Module GetModuleById(int id)
        {
            return dbContext.Modules.Include(c => c.Courses).Where(i => i.Id == id).FirstOrDefault();
        }

    }
}
