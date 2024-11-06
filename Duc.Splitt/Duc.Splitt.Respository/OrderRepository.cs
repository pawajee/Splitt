using Duc.Splitt.Core.Contracts.Repositories;
using Duc.Splitt.Data.DataAccess.Context;
using Duc.Splitt.Data.DataAccess.Models;
using Duc.Splitt.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;

namespace Duc.Splitt.Respository.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        protected readonly SplittAppContext _context;

        public OrderRepository(SplittAppContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Order?> GetOrderRequestByOrderId(Guid orderId)
        {
            var obj = _context.Order.
                Include(t => t.OrderItem).
                Include(t => t.OrderStatus).
                Include(t => t.Currency).
                Include(t => t.PaymentInstallment).
                ThenInclude(pi => pi.InstallmentType).
                Include(t => t.PaymentInstallment).
                ThenInclude(pi => pi.PaymentStatus).

                Where(t => t.Id == orderId);
            return await obj.FirstOrDefaultAsync();
          //  return null;

        }
    }
}
