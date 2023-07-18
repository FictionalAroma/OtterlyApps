using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Otterly.API.Configuration;

namespace Otterly.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Constants.TwitchAuthPolicyName)]
    public class TwitchController : ControllerBase
    {
        [HttpGet]
		public async Task<IActionResult> VerifyToken()
		{
			var token = await HttpContext.GetTokenAsync(Constants.TwitchAuthPolicyName, "access_token");
			return Ok(token);
		}
    }
}
