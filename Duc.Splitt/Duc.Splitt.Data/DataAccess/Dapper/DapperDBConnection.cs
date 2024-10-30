using Microsoft.Extensions.Configuration;
using System.Data;

namespace Duc.Splitt.Data.Dapper
{
    public class DapperDBConnection : IDapperDBConnection
    {
        private readonly IConfiguration _configuration;
        private readonly string? _connectionString;
        public DapperDBConnection(IConfiguration configuration)
        {
            _configuration = configuration;
            var conString = _configuration.GetConnectionString("DbConnectionStringSplitt");
            if (!string.IsNullOrEmpty(conString))
            {
                _connectionString = conString;
            }
        }

        public IDbConnection GetConnection()
        {
            return new Microsoft.Data.SqlClient.SqlConnection(_connectionString);
        }

        public void CloseConnection()
        {

        }
    }
}