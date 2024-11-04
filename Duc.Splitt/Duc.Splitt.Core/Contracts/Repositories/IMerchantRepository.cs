using Duc.Splitt.Data.DataAccess.Models;

namespace Duc.Splitt.Core.Contracts.Repositories
{
    public interface IMerchantRepository : IRepository<Merchant>
    {
        Task<Merchant?> GetMerchantRequest(Guid requestId);
        Task<Merchant?> GetMerchantRequestByEmail(string emailId);
    }
}
