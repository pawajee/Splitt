using Duc.Splitt.Core.Contracts.Repositories;
using Duc.Splitt.Data.DataAccess.Context;
using Duc.Splitt.Data.DataAccess.Models;
using Duc.Splitt.Repository;

namespace Duc.Splitt.Respository.Repository
{
    public class UserTypeRepository : Repository<UserType>, IUserTypeRepository
    {
        protected readonly SplittAppContext _context;

        public UserTypeRepository(SplittAppContext context) : base(context)
        {
            _context = context;
        }

    }
}
