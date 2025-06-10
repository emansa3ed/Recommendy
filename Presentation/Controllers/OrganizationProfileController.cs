using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api/profile")]
    [ApiController]
   // [Authorize]
    public class OrganizationProfileController : ControllerBase
    {
        private readonly IOrganizationProfileService _organizationProfileService;

        public OrganizationProfileController(IOrganizationProfileService organizationProfileService)
        {
            _organizationProfileService = organizationProfileService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrganizationProfile(string id)
        {
            var profile = await _organizationProfileService.GetOrganizationProfileAsync(id);
            return Ok(profile);
        }
    }
} 