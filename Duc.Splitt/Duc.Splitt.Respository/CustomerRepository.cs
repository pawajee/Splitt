using Duc.Splitt.Core.Contracts.Repositories;
using Duc.Splitt.Data.DataAccess.Context;
using Duc.Splitt.Data.DataAccess.Models;
using Duc.Splitt.Repository;
using Microsoft.EntityFrameworkCore;

namespace Duc.Splitt.Respository.Repository
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        protected readonly SplittAppContext _context;

        public CustomerRepository(SplittAppContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Customer?> GetConsumerUserByMobileNo(string mobileNo)
        {

            var obj = await _context.Customer.Include(t => t.User).Where(t => t.MobileNo == mobileNo).FirstOrDefaultAsync();
            return obj;
        }
    }
}
