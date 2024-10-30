using Duc.Splitt.Core.Contracts.Repositories;
using Duc.Splitt.Data.DataAccess.Context;
using Duc.Splitt.Data.DataAccess.Models;
using Duc.Splitt.Repository;

namespace Duc.Splitt.Respository.Repository
{
	public class MerchantAverageOrderRepository : Repository<MerchantAverageOrder>, IMerchantAverageOrderRepository
    {
        protected readonly SplittAppContext _context;

        public MerchantAverageOrderRepository(SplittAppContext context) : base(context)
        {
            _context = context;
        }

    }
}
