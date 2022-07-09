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
    public class CoursesDA : ICoursesDA
    {
        private DAISUni2Context dbContext;

        public CoursesDA(DAISUni2Context dbContext, IConfiguration configuration)
        {
            this.dbContext = dbContext;
        }

        public Course GetCourseDetailsById(int id)
        {
            return dbContext.Courses
                .Include(s => s.StudentsCourses)
                .Include(t => t.Teachers)
                .Include(m => m.Modules)
                .Include(ma => ma.Materials)
                .FirstOrDefault(i => i.Id == id);
        }

        public Course GetCouraeById(int id)
        {
            var course = dbContext.Courses.FirstOrDefault(c => c.Id == id);
            return course;
        }

        public Course GetEditCourseById(int id)
        {
            var course = dbContext.Courses.Include(t => t.Teachers).FirstOrDefault(i => i.Id == id);
            return course;
        }

        public Course GetCourseModules(int id)
        {
            return dbContext.Courses.Where(i => i.Id == id).Include(m => m.Modules).FirstOrDefault();
        }

        public IEnumerable<Course> GetCourses()
        {

            var courses = dbContext.Courses.Include(v => v.Votes).Include(s => s.StudentsCourses).ToList();

            return courses;
        }

        public Course GetCourseStudents(int id)
        {
            return dbContext.Courses.Where(i => i.Id == id).Include(s => s.StudentsCourses).FirstOrDefault();
        }

        public Course GetCourseMaterials(int id)
        {
            return dbContext.Courses.Where(i => i.Id == id).Include(m => m.Materials).FirstOrDefault();
        }

    }
}
