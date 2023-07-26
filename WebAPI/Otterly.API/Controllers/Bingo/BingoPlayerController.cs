using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Otterly.API.ClientLib.Bingo;
using Otterly.API.Configuration;
using Otterly.API.Handlers.Interfaces;
using Otterly.API.ManualMapper;

namespace Otterly.API.Controllers.Bingo
{
    [Route("api/bingo/player")]
    [ApiController]
	[Authorize(AuthenticationSchemes = Constants.TwitchAuthPolicyName)]
    public class BingoPlayerController : ControllerBase
    {
		private readonly IBingoGameHandler _handler;

		public BingoPlayerController(IBingoGameHandler handler)
		{
			_handler = handler;
		}

		[HttpPost]
		[Route("createTicket")]
		public async Task<IActionResult> CreatePlayerTicket(StreamerTicketRequest request)
		{
			if (string.IsNullOrWhiteSpace(request.PlayerTwitchID) || string.IsNullOrWhiteSpace(request.StreamerTwitchID))
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
				return Ok(existingTicket);
			}

			var result = await _handler.CreatePlayerTicket(request.PlayerTwitchID, session);
			return result != null ? Ok(GameMapper.Map(result)) : StatusCode(500, "unable to create ticket");
		}
		[HttpGet]
		[Route("getSession")]
		public async Task<IActionResult> GetCurrentSession(string streamerTwitchID)
		{
			return Ok(await _handler.GetCurrentSessionForStreamer(streamerTwitchID));
		}

		[HttpGet]
		[Route("getTicket")]
		public async Task<IActionResult> GetTicketData(string ticketID)
		{
			var session = await _handler.GetLatestCardData(ticketID);
			return session == null ? ValidationProblem("Streamer Bingo Not Active") : Ok(GameMapper.Map(session));
		}

		[HttpPost]
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
				return ValidationProblem("Ticket No Longer Valid");
			}

			var result = await _handler.MarkTicketItem(ticket, request.ItemIndex);
			return result.Success ? Ok(result) : StatusCode(500, result);
		}


    }
}
