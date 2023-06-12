using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Otterly.API.ClientLib.DTO;
using Otterly.API.Handlers.Interfaces;
using Otterly.Database;

namespace Otterly.API.Controllers;

[Authorize]
public class AccountController : ControllerBase
{
	private readonly IAccountHandler _actionHandler;
	private const string UUID_NAME = "uuid";

	public AccountController(IAccountHandler actionHandler) { _actionHandler = actionHandler; }

	
	[HttpGet]
	public async Task<IActionResult> GetUserAccount()
	{
		var claims = (ClaimsIdentity)this.User.Identity;
		if (claims == null) return Unauthorized();
		var uuidClaim =
			claims.FindFirst(claim => claim.Type.Contains(UUID_NAME, StringComparison.InvariantCultureIgnoreCase));
		if(uuidClaim == null) return Unauthorized();
		if (Guid.TryParse(uuidClaim.Value,
			out var userGuid))
		{
			var user = await _actionHandler.GetUserProfile(userGuid);
			return new JsonResult(user);
		}

		return Unauthorized();
	}

}