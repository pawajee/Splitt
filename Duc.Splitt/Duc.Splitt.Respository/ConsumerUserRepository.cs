using Duc.Splitt.Core.Contracts.Repositories;
using Duc.Splitt.Data.DataAccess.Context;
using Duc.Splitt.Data.DataAccess.Models;
using Duc.Splitt.Repository;
using Microsoft.EntityFrameworkCore;

namespace Duc.Splitt.Respository.Repository
{
    public class ConsumerUserRepository : Repository<ConsumerUser>, IConsumerUserRepository
    {
        protected readonly SplittAppContext _context;

        public ConsumerUserRepository(SplittAppContext context) : base(context)
        {
            _context = context;
        }
        public async Task<ConsumerUser?> GetConsumerUserByMobileNo(string mobileNo)
        {
            var obj = await _context.ConsumerUser.Include(t => t.User).Where(t => t.MobileNo == mobileNo).FirstOrDefaultAsync();
            return obj;
        }
    }
}
