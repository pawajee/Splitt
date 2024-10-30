using System.Data;

namespace Duc.Splitt.Data.Dapper
{

    public interface IDapperDBConnection
    {
        IDbConnection GetConnection();
        void CloseConnection();
    }


}
