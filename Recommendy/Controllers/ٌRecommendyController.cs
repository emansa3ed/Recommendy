using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Recommendy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class _ٌRecommendyController : ControllerBase
    {
        [HttpGet]
        public IActionResult test()
        {
            return Ok();
        }
    }
}
