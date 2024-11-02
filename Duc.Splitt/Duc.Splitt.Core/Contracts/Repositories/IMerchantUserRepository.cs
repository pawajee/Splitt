using Duc.Splitt.Data.DataAccess.Models;

namespace Duc.Splitt.Core.Contracts.Repositories
{
    public interface IMerchantUserRepository : IRepository<MerchantUser>
    {
        Task<MerchantUser?> GetMerchantRequestByEmail(string email);
        Task<MerchantUser?> GetMerchantRequestById(Guid Id);
    }
}
