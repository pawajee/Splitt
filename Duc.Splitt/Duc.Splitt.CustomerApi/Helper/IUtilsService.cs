using Duc.Splitt.Common.Dtos.Responses;
using System.Security.Claims;

namespace Duc.Splitt.CustomerApi.Helper
{

    public interface IUtilsService
    {
        Task<RequestHeader> ValidateRequest(HttpRequest request, ClaimsPrincipal? user);
    }


}

