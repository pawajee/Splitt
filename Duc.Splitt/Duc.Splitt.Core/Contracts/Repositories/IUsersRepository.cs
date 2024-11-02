using Duc.Splitt.Data.DataAccess.Models;

namespace Duc.Splitt.Core.Contracts.Repositories
{
    public interface IUsersRepository : IRepository<User>
    {
        Task<User?> GetUserById(Guid userId);
    }
}
