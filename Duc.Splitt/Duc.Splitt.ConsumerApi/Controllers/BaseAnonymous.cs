using Duc.Splitt.ConsumerApi.ActionFilters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Duc.Splitt.ConsumerApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableRateLimiting("Public")]
    [ValidateAnonymousClientAttribute]
    public class BaseAnonymous : ControllerBase
    {
    }
}
