using System.Data;

namespace Duc.SmartEv.ConsumerApp.Data.Dapper
{

    public interface IDapperDBConnectionOld
    {
        IDbConnection GetConnection { get; }
        void CloseConnection();
    }


}
