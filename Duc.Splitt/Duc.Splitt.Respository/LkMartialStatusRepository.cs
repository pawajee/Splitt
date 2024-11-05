
using Duc.Splitt.Core.Contracts.Repositories;
using Duc.Splitt.Data.DataAccess.Context;
using Duc.Splitt.Data.DataAccess.Models;
using Duc.Splitt.Repository;

namespace Duc.Splitt.Respository.Repository
{
	public class LkMartialStatusRepository : Repository<LkMartialStatus>, ILkMartialStatusRepository
    {
        protected readonly SplittAppContext _context;

        public LkMartialStatusRepository(SplittAppContext context) : base(context)
        {
            _context = context;
        }

    }
}

