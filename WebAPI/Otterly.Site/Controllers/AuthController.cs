using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Otterly.Site.Controllers;

public class AuthController : ControllerBase
{
	// GET
	public ActionResult Login(string returnUrl = "/")
	{
		var result = new ChallengeResult("Auth0", new AuthenticationProperties() { RedirectUri = returnUrl});
		return result;
	}

	public async Task<ActionResult> Logout()
	{
		await HttpContext.SignOutAsync();

		return new SignOutResult("Auth0", new AuthenticationProperties
										  {
											  RedirectUri = "/"
										  });
	}
	public ActionResult GetUserSignedIn()
	{
		
		if (User.Identity != null && User.Identity.IsAuthenticated)
		{
			var claims = ((ClaimsIdentity)this.User.Identity).Claims.Select(c =>
																				new { type = c.Type, value = c.Value })
															 .ToArray();
			//((ClaimsIdentity)this.User.Identity).BootstrapContext;

			return new JsonResult(new { isAuthenticated = true, claims = claims });
		}

        return new JsonResult(new { isAuthenticated = false });

	}

	public ActionResult LoginCallback()
	{
		if (User.Identity == null || !User.Identity.IsAuthenticated)
		{

			// if we arent authenticated, go away!
			return RedirectToAction("Index", "Home");
		}
		

		return RedirectToAction("Index", "Home");
		
	}

	public ActionResult LogoutCallback()
	{
		return RedirectToAction("Index", "Home");
	}

}