using Duc.Splitt.Core.Contracts.Repositories;
using Duc.Splitt.Data.DataAccess.Context;
using Duc.Splitt.Data.DataAccess.Models;
using Duc.Splitt.Repository;

namespace Duc.Splitt.Respository.Repository
{
    public class LocationRepository : Repository<Location>, ILocationRepository
    {
        protected readonly SplittAppContext _context;

        public LocationRepository(SplittAppContext context) : base(context)
        {
            _context = context;
        }

    }
}
