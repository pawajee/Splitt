using Duc.Splitt.Data.DataAccess.Models;

namespace Duc.Splitt.Core.Contracts.Repositories
{
    public interface IMerchantContactRepository : IRepository<MerchantContact>
    {
        Task<MerchantContact?> GetMerchantRequestByEmail(string email);
        Task<MerchantContact?> GetMerchantRequestById(Guid Id);
    }
}
