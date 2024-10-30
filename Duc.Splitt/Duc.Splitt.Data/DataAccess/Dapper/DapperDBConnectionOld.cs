using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace Duc.SmartEv.ConsumerApp.Data.Dapper
{
    public class DapperDBConnectionOld : IDapperDBConnectionOld
    {
        private IDbConnection _connection;
        private readonly IOptions<DapperConfig> _configs;

        public DapperDBConnectionOld(IOptions<DapperConfig> Configs)
        {
            this._configs = Configs;
        }

        public IDbConnection GetConnection
        {
            get
            {
                if (this._connection == null)
                {

                    this._connection = new SqlConnection(_configs.Value.DbConnectionStringConsumerApp);
                }
                if (this._connection.State != ConnectionState.Open)
                {
                    this._connection.Open();
                }
                return this._connection;
            }
        }

        public void CloseConnection()
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }
        }
    }



}
