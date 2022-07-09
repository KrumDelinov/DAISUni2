using DataAccsess.DbContexts;
using DataAccsess.Entities;
using DataAccsess.EntityModels;
using DataAccsess.Services.Contracts;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccsess.Services
{
    public class StudentsWithADO : UseAdoNet, IStudentsWithADO
    {


        public StudentsWithADO(IConfiguration configuration) : base(configuration)
        {

        }

        public IEnumerable<AddStudentsModel> AddStudentsModels(int courseId)
        {
            using (var connection = CreateConnection())
            {
                List<AddStudentsModel> students = new List<AddStudentsModel>();

                SqlCommand command = new SqlCommand(GetNoStudentsInCourse(courseId), connection);
                var param = command.Parameters.AddWithValue("courseId", courseId);

                var reader = command.ExecuteReader();
               
                while (reader.Read())
                {
                    AddStudentsModel studentsModel = new AddStudentsModel();
                    StudentFromDbReader(studentsModel, reader);

                    students.Add(studentsModel);
                }

                reader.Close();
                return students;
            }
        }

        public IEnumerable<StudentsCourse> StudentsCourses()
        {
            using(var connection = CreateConnection())
            {
                var students = new List<StudentsCourse>();
                SqlCommand command = new SqlCommand(GetStudentCourses(), connection);

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    StudentsCourse studentsCourse = new StudentsCourse();
                    StudentCoursesFromDbReader(studentsCourse, reader);
                    students.Add(studentsCourse);
                }

                reader.Close();
                return students;
            }
        }

        private void StudentCoursesFromDbReader(StudentsCourse studentsCourse, SqlDataReader reader)
        {
            studentsCourse.StudentsId = (int)reader["StudentsId"];
            studentsCourse.CoursesId = (int)reader["CoursesId"];
        }

        private void StudentFromDbReader(AddStudentsModel studentsModel, SqlDataReader reader)
        {
            studentsModel.Id = (int)reader["Id"];
            studentsModel.FullName = (string)reader["FullName"];

        }

        private string GetNoStudentsInCourse(int courseId)
        {
            var sqlQuey = @"
SELECT TOP (1000) [Id]
      ,[FirstName] + ' '+ [LastName] AS FullName
	  ,sc.[CoursesId]
  FROM [dbo].[Students] AS s
  LEFT JOIN dbo.StudentsCourses AS sc ON s.Id = sc.StudentsId
  WHERE sc.CoursesId != @courseId OR sc.CoursesId IS NULL
";

            return sqlQuey;
        }

        private string GetStudentCourses()
        {
            var sqlQuery = @"SELECT TOP (1000) [StudentsId]
            ,[CoursesId]
             FROM [dbo].[StudentsCourses]";
            return sqlQuery;
        }
    }
}
