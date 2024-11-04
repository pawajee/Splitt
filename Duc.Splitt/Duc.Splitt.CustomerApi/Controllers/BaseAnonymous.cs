using Duc.Splitt.CustomerApi.ActionFilters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Duc.Splitt.CustomerApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableRateLimiting("Public")]
    [ValidateAnonymousClientAttribute]
    public class BaseAnonymous : ControllerBase
    {
    }
}
