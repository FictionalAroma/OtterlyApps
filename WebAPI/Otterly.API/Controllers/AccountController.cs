using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Otterly.API.ClientLib.Account;
using Otterly.API.Handlers.Interfaces;

namespace Otterly.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
	private readonly IAccountHandler _actionHandler;

	public AccountController(IAccountHandler actionHandler) { _actionHandler = actionHandler; }

	[HttpGet]
	public async Task<IActionResult> GetUserAccount(Guid userID)
	{
		var user = await _actionHandler.GetUserDTO(userID);
		return user != null ? Ok(user) : Unauthorized();
	}
	
	[AllowAnonymous]
	[HttpPost]
	[Route("create")]
	public async Task<IActionResult> CreateUserAccount([FromBody]CreateUserRequest newUser)
	{
		var createdUser = await _actionHandler.CreateUser(newUser.Auth0ID, newUser.UserToCreate);
		return createdUser != null ? Ok(createdUser) : Unauthorized();
	}


}