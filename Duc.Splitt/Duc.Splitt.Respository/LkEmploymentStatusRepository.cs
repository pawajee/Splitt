
using Duc.Splitt.Core.Contracts.Repositories;
using Duc.Splitt.Data.DataAccess.Context;
using Duc.Splitt.Data.DataAccess.Models;
using Duc.Splitt.Repository;

namespace Duc.Splitt.Respository.Repository
{
	public class LkEmploymentStatusRepository : Repository<LkEmploymentStatus>, ILkEmploymentStatusRepository
    {
        protected readonly SplittAppContext _context;

        public LkEmploymentStatusRepository(SplittAppContext context) : base(context)
        {
            _context = context;
        }

    }
}

