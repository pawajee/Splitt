using Duc.Splitt.Core.Contracts.Repositories;
using Duc.Splitt.Data.DataAccess.Context;
using Duc.Splitt.Data.DataAccess.Models;
using Duc.Splitt.Repository;
using Microsoft.EntityFrameworkCore;

namespace Duc.Splitt.Respository.Repository
{
	public class UsersRepository : Repository<User>, IUsersRepository
    {
        protected readonly SplittAppContext _context;

        public UsersRepository(SplittAppContext context) : base(context)
        {
            _context = context;
        }
        public async Task<User?> GetUserById(Guid userId)
        {
            var obj =await _context.User.Include(t => t.UserType).Include(t => t.MerchantUserUser).Include(t=>t.BackOfficeUserUser).Include(t => t.ConsumerUserUser).Where(t => t.Id == userId).FirstOrDefaultAsync();
            return  obj;
        }
    }
}
