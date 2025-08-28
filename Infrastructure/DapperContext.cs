using System.Data;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace StudioIncantare.Infrastructure
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public IDbConnection CreateDbConnection()
            => new MySqlConnection(_connectionString);
    }
}

