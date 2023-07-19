using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Otterly.API.ClientLib.Twitch;
using Otterly.API.Configuration;
using Otterly.API.ExtensionMethods;

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

			JwtSecurityToken parsedToken = new JwtSecurityToken(token);

			var prof = new ExtensionUser()
			{
				UserID = parsedToken.Claims.GetClaimValue("opaque_user_id"),
				BroadcasterID = parsedToken.Claims.GetClaimValue("channel_id"),
				UserRole = EnumExtensions.GetValueFromDescription<Role>(parsedToken.Claims.GetClaimValue("role")),
			};
			return Ok(prof);
		}
    }
}
