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
    public class MaterialsDA : IMaterialsDA
    {
        private DAISUni2Context dbContext;

        public MaterialsDA(DAISUni2Context dbContext, IConfiguration configuration)
        {
            this.dbContext = dbContext;
        }

        public ICollection<Material> AllMaterials()
        {
            return this.dbContext.Materials.Include(c => c.Courses).ToList();
        }

        public Material GetMaterialById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
