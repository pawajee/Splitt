using Duc.Splitt.Core.Contracts.Repositories;
using Duc.Splitt.Data.DataAccess.Context;
using Duc.Splitt.Data.DataAccess.Models;
using Duc.Splitt.Repository;
using Microsoft.EntityFrameworkCore;

namespace Duc.Splitt.Respository.Repository
{
    public class MerchantRequestRepository : Repository<MerchantRequest>, IMerchantRequestRepository
    {
        protected readonly SplittAppContext _context;

        public MerchantRequestRepository(SplittAppContext context) : base(context)
        {
            _context = context;
        }
        public async Task<MerchantRequest?> GetMerchantRequest(Guid requestId)
        {
            var obj = _context.MerchantRequest.Include(t => t.MerchantRequestAttachment).Include(t => t.MerchantRequestHistory).Where(t => t.Id == requestId);
            return await obj.FirstOrDefaultAsync();
        }

    }
}
