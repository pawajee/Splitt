using Duc.Splitt.Data.DataAccess.Models;

namespace Duc.Splitt.Core.Contracts.Repositories
{
    public interface IConsumerUserRepository : IRepository<ConsumerUser>
    {
        Task<ConsumerUser?> GetConsumerUserByMobileNo(string mobileNo);
    }
}
