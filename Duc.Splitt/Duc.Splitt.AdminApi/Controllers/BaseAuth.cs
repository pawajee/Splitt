using Duc.Splitt.MerchantApi.ActionFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Duc.Splitt.MerchantApi.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    [ValidateSecureClientAttribute]
    [EnableRateLimiting("Secure")]
    public class BaseAuth : ControllerBase
    {
    }
}
