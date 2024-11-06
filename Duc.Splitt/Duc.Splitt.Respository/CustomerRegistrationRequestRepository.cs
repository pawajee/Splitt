
using Duc.Splitt.Core.Contracts.Repositories;
using Duc.Splitt.Data.DataAccess.Context;
using Duc.Splitt.Data.DataAccess.Models;
using Duc.Splitt.Repository;
using Microsoft.EntityFrameworkCore;

namespace Duc.Splitt.Respository.Repository
{
    public class CustomerRegistrationRequestRepository : Repository<CustomerRegistrationRequest>, ICustomerRegistrationRequestRepository
    {
        protected readonly SplittAppContext _context;

        public CustomerRegistrationRequestRepository(SplittAppContext context) : base(context)
        {
            _context = context;
        }
        public async Task<int?> CustomerRegistrationRequestWithin15Min(string civilId)
        {
            var fiveMinutesAgo = DateTime.UtcNow.AddMinutes(-15);//ToDo

            var obj = _context.CustomerRegistrationRequest.Where(t => t.CivilId == civilId && t.CreatedOn >= fiveMinutesAgo).OrderByDescending(t => t.CreatedOn).CountAsync();
            return await obj;

        }
        public async Task<CustomerRegistrationRequest?> CustomerRegistrationRequestById(Guid id)
        {
            var obj = await _context.CustomerRegistrationRequest.Include(t => t.MidRequestLog).Where(t => t.Id == id).FirstOrDefaultAsync();
            return obj;


        }
        public async Task<int?> CustomerRegistrationRequestStatusId(Guid id)
        {
            var obj = await _context.CustomerRegistrationRequest.FirstOrDefaultAsync();
            return obj?.CustomerRegistrationStatusId;


        }
    }
}

