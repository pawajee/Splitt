using Duc.Splitt.MerchantApi.ActionFilters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Duc.Splitt.MerchantApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableRateLimiting("Public")]
    [ValidateAnonymousClientAttribute]
    public class BaseAnonymous : ControllerBase
    {
    }
}
