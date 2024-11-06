using Duc.Splitt.Data.DataAccess.Models;

namespace Duc.Splitt.Core.Contracts.Repositories
{
    public interface IOtpRequestRepository : IRepository<OtpRequest>
    {
        Task<OtpRequest?> GetLatestOtpRequestByMobileNo(string mobileNo);
        Task<OtpRequest?> GetOtpRequestById(Guid Id);
    }
}
