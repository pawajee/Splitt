using Duc.Splitt.Data.DataAccess.Models;

namespace Duc.Splitt.Core.Contracts.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        public Task<Order?> GetOrderRequestByOrderId(Guid requestId);
    }
}
