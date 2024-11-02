using Duc.Splitt.Core.Contracts.Repositories;
using Duc.Splitt.Data.DataAccess.Context;
using Duc.Splitt.Data.DataAccess.Models;
using Duc.Splitt.Repository;
using Microsoft.EntityFrameworkCore;

namespace Duc.Splitt.Respository.Repository
{
    public class MerchantUserRepository : Repository<MerchantUser>, IMerchantUserRepository
    {
        protected readonly SplittAppContext _context;

        public MerchantUserRepository(SplittAppContext context) : base(context)
        {
            _context = context;
        }
        public async Task<MerchantUser?> GetMerchantRequestByEmail(string email)
        {
            var obj = await _context.MerchantUser.Include(t => t.MerchantRequest).Where(t => t.BusinessEmail == email).FirstOrDefaultAsync();
            return obj;
        }
        public async Task<MerchantUser?> GetMerchantRequestById(Guid Id)
        {
            var obj = await _context.MerchantUser.Include(t => t.MerchantRequest).Where(t => t.MerchantRequestId == Id).FirstOrDefaultAsync();
            return obj;
        }
    }
}
