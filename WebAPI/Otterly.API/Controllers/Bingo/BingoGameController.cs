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
		var result = await _handler.CreateSession(request.UserID, request.CardID);
		return Ok(result);
	}
	[HttpPost]
	public async Task<IActionResult> CreatePlayerTicket()
	{
		return Ok();
	}
	[HttpPost]
	public async Task<IActionResult> GetCurrentSession()
	{
		return Ok();
	}
	[HttpPost]
	public async Task<IActionResult> GetTicketData()
	{
		return Ok();
	}


}