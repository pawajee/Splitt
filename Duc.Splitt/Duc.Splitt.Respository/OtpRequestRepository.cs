using Duc.Splitt.Core.Contracts.Repositories;
using Duc.Splitt.Data.DataAccess.Context;
using Duc.Splitt.Data.DataAccess.Models;
using Duc.Splitt.Repository;
using Microsoft.EntityFrameworkCore;

namespace Duc.Splitt.Respository.Repository
{
    public class OtpRequestRepository : Repository<OtpRequest>, IOtpRequestRepository
    {
        protected readonly SplittAppContext _context;

        public OtpRequestRepository(SplittAppContext context) : base(context)
        {
            _context = context;
        }

        public async Task<OtpRequest?> GetLatestOtpRequestByMobileNo(string mobileNo)
        {
            var obj = await _context.OtpRequest.Where(t => t.MobileNo == mobileNo).OrderByDescending(t => t.CreatedOn).FirstOrDefaultAsync();
            return obj;
        }
    }
}
