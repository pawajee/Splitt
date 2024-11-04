using Duc.Splitt.Core.Contracts.Repositories;
using Duc.Splitt.Data.DataAccess.Context;
using Duc.Splitt.Data.DataAccess.Models;
using Duc.Splitt.Repository;
using Microsoft.EntityFrameworkCore;

namespace Duc.Splitt.Respository.Repository
{
    public class MerchantRepository : Repository<Merchant>, IMerchantRepository
    {
        protected readonly SplittAppContext _context;

        public MerchantRepository(SplittAppContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Merchant?> GetMerchantRequest(Guid requestId)
        {
            var obj = _context.Merchant.Include(t => t.MerchantAttachment).Include(t => t.MerchantHistory).Include(t => t.MerchantContact.Where(t => t.IsPrimary == true)).
                Include(t => t.MerchantBusinessType).Include(t => t.MerchantAnnualSales).
                 Include(t => t.MerchantCategory).Include(t => t.MerchantStatus).
                  Include(t => t.MerchantAverageOrder).Include(t => t.Country).
                Where(t => t.Id == requestId);
            return await obj.FirstOrDefaultAsync();

        }
        public async Task<Merchant?> GetMerchantRequestByEmail(string emailId)
        {
            var obj = _context.Merchant.Include(t => t.MerchantAttachment).Include(t => t.MerchantHistory).Include(t => t.MerchantContact.Where(t => t.IsPrimary == true)).
                Include(t => t.MerchantBusinessType).Include(t => t.MerchantAnnualSales).
                 Include(t => t.MerchantCategory).Include(t => t.MerchantStatus).
                  Include(t => t.MerchantAverageOrder).Include(t => t.Country).
                Where(t => t.BusinessEmail == emailId);
            return await obj.FirstOrDefaultAsync();

        }

    }
}
