using Duc.Splitt.MIDCallbackAPI.ActionFilters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Duc.Splitt.MIDCallbackAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableRateLimiting("Public")]
    [ValidateAnonymousClientAttribute]
    public class BaseAnonymous : ControllerBase
    {
    }
}
