using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccsess.EntityModels
{
    public class AddStudentsModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        public int CourseId { get; set; }
    }
}
