using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Otterly.API.ClientLib.Bingo;
using Otterly.API.DataObjects.Bingo;
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

	public BingoGameController(IBingoGameHandler handler)
	{
		_handler = handler;
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
		return result.Success ? StatusCode(500, result) : Ok(result);
	}
	[HttpPost]
	[Route("createTicket")]
	public async Task<IActionResult> CreatePlayerTicket(StreamerTicketRequest request)
	{
		if (request.PlayerTwitchID == Guid.Empty || request.StreamerTwitchID == Guid.Empty)
		{
			return BadRequest();
		}

		var session = await _handler.GetCurrentSessionForStreamer(request.StreamerTwitchID);
		if (session == null || string.IsNullOrEmpty(session.Id))
		{
			return ValidationProblem("Streamer Bingo Not Active");
		}

		var existingTicket = await _handler.GetTicketForPlayer(request.PlayerTwitchID, session.Id);
		if (existingTicket != null)
		{
			return Conflict(existingTicket);
		}

        var result = await _handler.CreatePlayerTicket(request.PlayerTwitchID, session);
		return result != null ? Ok(GameMapper.Map(result)) : StatusCode(500, "unable to create ticket");
	}
    [HttpGet]
	[Route("getSession")]
	public async Task<IActionResult> GetCurrentSession(Guid streamerTwitchID)
	{
		var session = await _handler.GetCurrentSessionForStreamer(streamerTwitchID);
		return session == null ? ValidationProblem("Streamer Bingo Not Active") : Ok(GameMapper.Map(session));
	}

	[HttpGet]
	[Route("getTicket")]
	public async Task<IActionResult> GetTicketData(string ticketID)
	{
		var session = await _handler.GetLatestCardData(ticketID);
		return session == null ? ValidationProblem("Streamer Bingo Not Active") : Ok(GameMapper.Map(session));
	}

	[HttpGet]
	[Route("GetSessionAndTicket")]
	public async Task<IActionResult> GetSessionAndTicketData(StreamerTicketRequest request)
	{
		InitialSetupResponse resp = new InitialSetupResponse();
		var session = await _handler.GetCurrentSessionForStreamer(request.StreamerTwitchID);
		if (session == null || string.IsNullOrEmpty(session.Id))
		{
			return Ok(resp);
		}

		resp.Session = GameMapper.Map(session);
		var ticket = await _handler.GetTicketForPlayer(request.PlayerTwitchID, session.Id);
		if (ticket != null)
		{
			resp.PlayerTicket = GameMapper.Map(ticket);
		}
		return Ok(resp);
	}

	[HttpPost]
	[Route("markItem")]
	public async Task<IActionResult> MarkItemOnTicket(MarkItemRequest request)
	{
		var ticket = await _handler.GetTicketForPlayer(request.PlayerTwitchID, request.SessionID);
		if (ticket == null)
		{
			return StatusCode(500, "Ticket No Longer Valid");
		}

		var result = await _handler.MarkTicketItem(ticket, request.ItemIndex);
		return result.Success ? Ok(result) : StatusCode(500, result);
	}
	
	[HttpPost]
	[Route("verifyItem")]
	public async Task<IActionResult> VerifyItemInSession(VerifyItemRequest request)
	{
		var session = await _handler.GetSessionData(request.SessionID);
		if (session == null || !session.Active )
		{
			return StatusCode(500, "Session No Longer Valid");
		}

		var result = await _handler.VerifySessionItem(session, request.ItemIndex);
		return Ok(result);

	}
	[HttpPost]
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