using DataAccsess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccsess.Services.Contracts
{
    public interface IStudentsDA
    {

        ICollection<Student> GetAllNoDeletedStudnts();

        Task<Student> FindByIdAsunc(int id);

        Student FindById(int id);

        
    }
}
