
using Duc.Splitt.Core.Contracts.Repositories;
using Duc.Splitt.Data.DataAccess.Context;
using Duc.Splitt.Data.DataAccess.Models;
using Duc.Splitt.Repository;
using Microsoft.EntityFrameworkCore;

namespace Duc.Splitt.Respository.Repository
{
	public class MerchantContactRepository : Repository<MerchantContact>, IMerchantContactRepository
    {
        protected readonly SplittAppContext _context;

        public MerchantContactRepository(SplittAppContext context) : base(context)
        {
            _context = context;
        }

        public async Task<MerchantContact?> GetMerchantRequestByEmail(string email)
        {
            var obj = await _context.MerchantContact.Include(t => t.MerchantRequest).Where(t => t.BusinessEmail == email).FirstOrDefaultAsync();
            return obj;
        }

        public async Task<MerchantContact?> GetMerchantRequestById(Guid Id)
        {
            var obj = await _context.MerchantContact.Include(t => t.MerchantRequest).Where(t => t.MerchantRequestId == Id).FirstOrDefaultAsync();
            return obj;
        }

        //public async Task<MerchantContact?> GetMerchantRequestByEmail(string email)
        //{
        //    var obj = await _context.MerchantContact.Include(t => t.MerchantRequest).Where(t => t.BusinessEmail == email).FirstOrDefaultAsync();
        //    return obj;
        //}
        //public async Task<MerchantContact?> GetMerchantRequestById(Guid Id)
        //{
        //    var obj = await _context.MerchantContact.Include(t => t.MerchantRequest).Where(t => t.MerchantRequestId == Id).FirstOrDefaultAsync();
        //    return obj;
        //}
    }
}

