﻿using Duc.Splitt.Data.DataAccess.Models;

namespace Duc.Splitt.Core.Contracts.Repositories
{
    public interface IMerchantRequestRepository : IRepository<MerchantRequest>
    {
        Task<MerchantRequest?> GetMerchantRequest(Guid requestId);
    }
}
