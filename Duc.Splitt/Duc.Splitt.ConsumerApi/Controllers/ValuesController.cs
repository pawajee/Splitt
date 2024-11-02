using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Duc.Splitt.ConsumerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return DateTime.Now.ToString();
        }
    }
}
