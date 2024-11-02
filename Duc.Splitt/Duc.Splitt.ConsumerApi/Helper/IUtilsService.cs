using Duc.Splitt.Common.Dtos.Responses;
using System.Security.Claims;

namespace Duc.Splitt.MerchantApi.Helper
{

    public interface IUtilsService
    {
        Task<RequestHeader> ValidateRequest(HttpRequest request, ClaimsPrincipal? user);
    }


}

