using DataAccsess.DbContexts;
using DataAccsess.Entities;
using DataAccsess.Services.Contracts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace DataAccsess.Services
{
    public class TeachersDA : ITeachersDA
    {
        private DAISUni2Context dbContext;

        public TeachersDA(DAISUni2Context dbContext, IConfiguration configuration)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Teacher> GetTachers(int maxCount)
        {
            if (maxCount > 10000) maxCount = 10000;
            if (maxCount <= 0) maxCount = 10;

            return dbContext.Teachers.Include(x => x.Users).Take(maxCount).ToList();
        }

        public async Task<Teacher> FindTeacherByIdAsynk(int id)
        {
            var teacher = await dbContext.Teachers.FindAsync(id);
            return teacher;
        }

        public Teacher GetTeacherById(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection("Data Source=DESKTOP-E2HSE8H;Initial Catalog=DAISUni2;Integrated Security=True"))
            using (SqlCommand cmd = sqlConnection.CreateCommand())
            {
                cmd.CommandText = @"
                        SELECT TOP (1000) [Id]
                              ,[FirstName]
                              ,[LastName]
                              ,[Email]
                              ,[HiredOn]
                              ,[CreatedOn]
                          FROM [DAISUni2].[dbo].[Teachers]
                          WHERE Id = @Id
                        ";

                var param = cmd.Parameters.Add("Id", System.Data.SqlDbType.Int);
                param.Value = id;
                sqlConnection.Open();

                var reader = cmd.ExecuteReader();

                var teacher = new Teacher();

                while (reader.Read())
                {

                    teacher.FirstName = (string)reader["FirstName"];
                    teacher.LastName = (string)reader["LastName"];
                    teacher.Email = (string)reader["Email"];
                    teacher.HiredOn = (DateTime)reader["HiredOn"];
                    teacher.CreatedOn = (DateTime)reader["CreatedOn"];


                }

                return teacher;
            }


        }

        public void UpdateTeacher(Teacher teacher)
        {

            dbContext.Teachers.Update(teacher);
        }

        public async Task SaveChagesAsync()
        {
            await dbContext.SaveChangesAsync();
        }

        public bool TeacherExists(int id)
        {
            return (dbContext.Teachers?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public void AddTeacher(Teacher teacher)
        {
            dbContext.Teachers.Add(teacher);
        }
    }
}