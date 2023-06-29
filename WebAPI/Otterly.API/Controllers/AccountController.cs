using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Otterly.API.Handlers.Interfaces;

namespace Otterly.API.Controllers;

[Authorize]
public class AccountController : ControllerBase
{
	private readonly IAccountHandler _actionHandler;
	private const string UUID_NAME = "uuid";

	public AccountController(IAccountHandler actionHandler) { _actionHandler = actionHandler; }

	
	[HttpGet]
	public async Task<IActionResult> GetUserAccount(Guid userID)
	{
			var user = await _actionHandler.GetUserProfile(userGuid);

		return user != null ? Ok(user) : Unauthorized();
	}

}