using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Otterly.Site.Controllers;

public class AuthController : ControllerBase
{
	// GET
	public ActionResult Login(string returnUrl = "/")
	{
		return new ChallengeResult("Auth0", new AuthenticationProperties() { RedirectUri = returnUrl});
	}

	[Authorize]
	public async Task<ActionResult> Logout()
	{
		await HttpContext.SignOutAsync();

		return new SignOutResult("Auth0", new AuthenticationProperties
										  {
											  RedirectUri = "/"
										  });
	}

	[Authorize]
	public ActionResult GetUser()
	{
		if (User.Identity.IsAuthenticated)
		{
			var claims = ((ClaimsIdentity)this.User.Identity).Claims.Select(c =>
																				new { type = c.Type, value = c.Value })
															 .ToArray();

			return new JsonResult(new { isAuthenticated = true, claims = claims });
		}

		return new JsonResult(new { isAuthenticated = false });
	}

	public ActionResult Callback()
	{
		return RedirectToAction("Index", "Home");
	}
}