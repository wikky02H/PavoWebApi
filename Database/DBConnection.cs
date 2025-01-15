using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace PavoWeb.Database
{
    public class DBConnection
    {
        private readonly string _connectionString;

        public DBConnection(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? 
                throw new ArgumentException("Connection string 'DefaultConnection' is missing or empty."); ;

            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new ArgumentException("Connection string 'DefaultConnection' is missing or empty.");
            }
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
