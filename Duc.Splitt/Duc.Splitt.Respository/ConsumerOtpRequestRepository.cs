using Duc.Splitt.Core.Contracts.Repositories;
using Duc.Splitt.Data.DataAccess.Context;
using Duc.Splitt.Data.DataAccess.Models;
using Duc.Splitt.Repository;
using Microsoft.EntityFrameworkCore;

namespace Duc.Splitt.Respository.Repository
{
    public class ConsumerOtpRequestRepository : Repository<ConsumerOtpRequest>, IConsumerOtpRequestRepository
    {
        protected readonly SplittAppContext _context;

        public ConsumerOtpRequestRepository(SplittAppContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ConsumerOtpRequest?> GetLatestOtpRequestByMobileNo(string mobileNo)
        {
            var obj = await _context.ConsumerOtpRequest.Where(t => t.MobileNo == mobileNo).OrderByDescending(t => t.CreatedOn).FirstOrDefaultAsync();
            return obj;
        }
    }
}
