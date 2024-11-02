using Duc.Splitt.BackOfficeApi.ActionFilters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Duc.Splitt.BackOfficeApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableRateLimiting("Public")]
    [ValidateAnonymousClientAttribute]
    public class BaseAnonymous : ControllerBase
    {
    }
}
