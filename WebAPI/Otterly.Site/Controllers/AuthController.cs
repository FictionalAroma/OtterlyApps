using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Auth0.ManagementApi.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Http;
using Otterly.API.ClientLib;
using Otterly.API.ClientLib.Account;
using Otterly.Site.ManualMappers;

namespace Otterly.Site.Controllers;

[Route("bff/[controller]/[action]")]
public class AuthController : APILinkController
{
	public AuthController(ITypedHttpClientFactory<OtterlyAPIClient> httpClientFactory, HttpClient baseClient) : base(httpClientFactory, baseClient)
	{

	}

	[HttpGet]
	// GET
	public ActionResult Login([FromQuery]string? returnUrl = "/")
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

	[HttpPost]
	[Route("NewUser")]
	public async Task<IActionResult> AddNewUser(User newUser)
	{
		var apiClient = await GenerateClientAsync();
		var createdUser = await apiClient.CreateUser(new CreateUserRequest
													 {
														 Auth0ID = newUser.UserId,
														 UserToCreate = Auth0Mapper.Map(newUser)
													 });

		return Ok();
	}

}