using Duc.Splitt.Data.DataAccess.Models;

namespace Duc.Splitt.Core.Contracts.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetUserById(Guid userId);
    }
}
