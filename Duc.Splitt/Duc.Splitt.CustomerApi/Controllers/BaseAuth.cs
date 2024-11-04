using Duc.Splitt.CustomerApi.ActionFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Duc.Splitt.CustomerApi.Controllers
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
