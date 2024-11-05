using Duc.Splitt.Core.Contracts.Repositories;
using Duc.Splitt.Data.DataAccess.Context;
using Duc.Splitt.Data.DataAccess.Models;
using Duc.Splitt.Repository;

namespace Duc.Splitt.Respository.Repository
{
	public class LkCurrencyRepository : Repository<LkCurrency>, ILkCurrencyRepository
    {
        protected readonly SplittAppContext _context;

        public LkCurrencyRepository(SplittAppContext context) : base(context)
        {
            _context = context;
        }

    }
}
