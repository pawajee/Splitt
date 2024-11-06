
using Duc.Splitt.Data.DataAccess.Models;

namespace Duc.Splitt.Core.Contracts.Repositories
{
    public interface ICustomerRegistrationRequestRepository : IRepository<CustomerRegistrationRequest>
    {
        Task<int?> CustomerRegistrationRequestWithin15Min(string civilId);
        Task<CustomerRegistrationRequest?> CustomerRegistrationRequestById(Guid id);
        Task<int?> CustomerRegistrationRequestStatusId(Guid id);
    }
}

