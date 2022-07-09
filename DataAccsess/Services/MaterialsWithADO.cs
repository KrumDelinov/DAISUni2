using DataAccsess.DbContexts;
using DataAccsess.Entities;
using DataAccsess.Services.Contracts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccsess.Services
{
    public class MaterialsWithADO : UseAdoNet, IMaterialsWithADO
    {
        private DAISUni2Context dbContext;

        public MaterialsWithADO(IConfiguration configuration) : base(configuration)
        {
            dbContext = new DAISUni2Context();
        }



        public Material GetMaterialById(int id)
        {
            using (var connection = CreateConnection())
            {
                SqlCommand command = new SqlCommand(GetSelectMaterialaById(), connection);
                var param = command.Parameters.AddWithValue("Id", id);

                Material material = new Material();

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    MaterialFromDbReader(material, reader);
                }
                return material;
            }
        }

        private void MaterialFromDbReader(Material material, SqlDataReader reader)
        {
            material.Id = (int)reader["Id"];
            material.MaterialType = (int)reader["MaterialType"];
            material.MaterialUrl = reader["MaterialUrl"] == DBNull.Value ? null : (string)reader["MaterialUrl"];
            material.Courses = new Course
            {
                Name = (string)reader["Name"]
            };


        }

        public string GetSelectMaterialaById()
        {
            return @"

                SELECT m.Id
                     ,MaterialType
                     ,MaterialUrl
                     ,m.CoursesId
	                 ,c.Name
                FROM [DAISUni2].[dbo].[Materials] AS m
                INNER JOIN [dbo].Courses AS c
                ON m.CoursesId = c.[Id]
                WHERE m.Id = @Id ";
        }




    }
}
