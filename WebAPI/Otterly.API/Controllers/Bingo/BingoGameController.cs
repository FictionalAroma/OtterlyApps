using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Otterly.API.ClientLib.Bingo;
using Otterly.API.Handlers.Interfaces;

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

	public BingoGameController(IBingoGameHandler handler) { _handler = handler; }

	[HttpPost]
	[Route("startnew")]
	public async Task<IActionResult> CreateNewSession(CreateSessionRequest request)
	{
		if (request.UserID == Guid.Empty)
		{
			return BadRequest();
		}
		var result = await _handler.CreateSession(request.UserID, request.CardID);
		return result.Success ? StatusCode(500, result) : Ok(result);
	}
	[HttpPost]
	[Route("createTicket")]

	public async Task<IActionResult> CreatePlayerTicket(CreateTicketRequest request)
	{
		if (request.PlayerTwitchID == Guid.Empty || request.StreamerTwitchID == Guid.Empty)
		{
			return BadRequest();
		}
		var session = await _handler.GetCurrentSessionForStreamer(request.StreamerTwitchID);
		if (session == null)
		{
			return ValidationProblem("Streamer Bingo Not Active");
		}
        var result = await _handler.CreatePlayerTicket(request.PlayerTwitchID, session);
        return result.Success ? StatusCode(500, result) : Ok(result);
	}
    [HttpGet]
	[Route("getSession")]

	public async Task<IActionResult> GetCurrentSession(Guid streamerTwitchID)
	{
		var session = await _handler.GetCurrentSessionForStreamer(streamerTwitchID);
		return session == null ? ValidationProblem("Streamer Bingo Not Active") : Ok(session);
	}
	[HttpPost]
	[Route("getTicket")]

	public async Task<IActionResult> GetTicketData()
	{
		return Ok();
	}


}