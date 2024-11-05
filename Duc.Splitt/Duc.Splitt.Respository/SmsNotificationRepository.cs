
using Duc.Splitt.Core.Contracts.Repositories;
using Duc.Splitt.Data.DataAccess.Context;
using Duc.Splitt.Data.DataAccess.Models;
using Duc.Splitt.Repository;

namespace Duc.Splitt.Respository.Repository
{
	public class SmsNotificationRepository : Repository<SmsNotification>, ISmsNotificationRepository
    {
        protected readonly SplittAppContext _context;

        public SmsNotificationRepository(SplittAppContext context) : base(context)
        {
            _context = context;
        }

    }
}

