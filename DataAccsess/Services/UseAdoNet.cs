using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccsess.Services
{
    public class UseAdoNet
    {
        protected readonly IConfiguration configuration;
        protected string connectionString = null;

        public UseAdoNet(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.connectionString = this.configuration.GetConnectionString("LocalDb");
        }

        protected SqlConnection CreateConnection()
        {
            //return new SqlConnection(this.connectionString);
            var connection = new SqlConnection(this.configuration.GetConnectionString("LocalDb"));
            connection.Open();
            return connection;
        }
    }
}
