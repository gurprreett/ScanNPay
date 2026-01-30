using Microsoft.Data.SqlClient;
using System.Data;
//using System.Data.SqlClient;
namespace ScanNPay.Repository
{
    public class dbContext
    {
        private readonly IConfiguration _configuration;
        private readonly string? _connectionString;

        public dbContext(IConfiguration configuration)
        {
            _configuration = configuration;
            //_connectionString = _configuration.GetConnectionString("DefaultConnection");
            _connectionString = _configuration.GetConnectionString("AWSServer");
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);

    }
}