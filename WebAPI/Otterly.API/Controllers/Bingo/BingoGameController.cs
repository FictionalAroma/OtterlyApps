using System;
using System.Threading.Tasks;
using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Http;
using Otterly.API.ClientLib.Bingo;
using Otterly.API.Configuration;
using Otterly.API.ExternalAPI;
using Otterly.API.Handlers.Interfaces;
using Otterly.API.ManualMapper;

namespace Otterly.API.Controllers.Bingo;
[Authorize]
[Route("api/bingo/game")]
[ApiController]
public class BingoGameController : ControllerBase
{
	private readonly IBingoGameHandler _handler;

	// GET
	// ReSharper disable once InconsistentNaming
	private static readonly Guid TEST_USER = new Guid("e3aeefa0-b2df-4af7-9033-914ef6936bf0");
	private readonly ITypedHttpClientFactory<TwitchExtensionAPIConnector> _twitchClientFactory;

	public BingoGameController(IBingoGameHandler handler, 
							   ITypedHttpClientFactory<TwitchExtensionAPIConnector> twitchClientFactory)
	{
		_handler = handler;
		_twitchClientFactory = twitchClientFactory;
	}

	[HttpPost]
	[Route("startnew")]
	public async Task<IActionResult> CreateNewSession(CreateSessionRequest request)
	{
		if (request.UserID == Guid.Empty)
		{
			return BadRequest();
		}
		var result = await _handler.CreateSession(request.UserID, request.CardID);
		return result.Success ? Ok(result) : StatusCode(500, result);
		
	}

	[HttpGet]
	[Route("getSessionMeta")]
	public async Task<IActionResult> GetCurrentSessionMetaData(Guid userID)
	{
		return Ok(await _handler.GetCurrentSessionMeta(userID));
	}


	[HttpGet]
	[Route("getSessionForUser")]
	public async Task<IActionResult> GetCurrentSession(Guid userID)
	{
		return Ok(await _handler.GetCurrentSessionForUser(userID));
	}


	[HttpPost]
	[Authorize(AuthenticationSchemes = $"{Constants.TwitchAuthPolicyName}, {Constants.Auth0PolicyName}")]
	[Route("verifyItem")]
	public async Task<IActionResult> VerifyItemInSession(VerifyItemRequest request)
	{
		var session = await _handler.GetSessionData(request.SessionID);
		if (session == null || !session.Active )
		{
			return StatusCode(500, "Session No Longer Valid");
		}

		var result = await _handler.VerifySessionItem(session, request.ItemIndex, request.State);

		var twtichClient = _twitchClientFactory.GetClient();
		await twtichClient.SendExtensionMessage(request, session.TwitchUserID);

		return Ok(result);

	}
	[HttpGet]
	[Route("endSession")]
	public async Task<IActionResult> EndSession(string sessionID)
	{
		var session = await _handler.GetSessionData(sessionID);
		if (session == null || !session.Active )
		{
			return StatusCode(500, "Session No Longer Valid");
		}

		var result = await _handler.EndSession(session);
		return Ok(result);

	}

}