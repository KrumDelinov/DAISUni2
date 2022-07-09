using DataAccsess.DbContexts;
using DataAccsess.Entities;
using DataAccsess.Services.Contracts;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccsess.Services
{
    public class StudentsDA : IStudentsDA
    {
        private DAISUni2Context dbContext;

        public StudentsDA(DAISUni2Context dbContext, IConfiguration configuration)
        {
            this.dbContext = dbContext;
        }

        public Student FindById(int id)
        {
            return dbContext.Students.Where(i => i.Id == id).FirstOrDefault();
        }

        public async Task<Student> FindByIdAsunc(int id)
        {
            return await dbContext.Students.FindAsync(id);
        }

        public ICollection<Student> GetAllNoDeletedStudnts()
        {
            var stidents = dbContext.Students.Where(x => x.IsDeleted == false).ToList();
            return stidents;
        }

        public bool StudentExists(int id)
        {
            return (dbContext.Students?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
