using Duc.Splitt.Core.Contracts.Repositories;
using Duc.Splitt.Data.DataAccess.Context;
using Duc.Splitt.Data.DataAccess.Models;
using Duc.Splitt.Repository;

namespace Duc.Splitt.Respository.Repository
{
    public class MerchantRequestRepository : Repository<MerchantRequest>, IMerchantRequestRepository
    {
        protected readonly SplittAppContext _context;

        public MerchantRequestRepository(SplittAppContext context) : base(context)
        {
            _context = context;
        }

    }
}
