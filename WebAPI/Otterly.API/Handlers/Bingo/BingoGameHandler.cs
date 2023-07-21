using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Http;
using Otterly.API.ClientLib;
using Otterly.API.ClientLib.Bingo;
using Otterly.API.DataObjects.Bingo;
using Otterly.API.ExternalAPI;
using Otterly.API.Handlers.Interfaces;
using Otterly.API.ManualMapper;
using Otterly.Database.ActivityData.Bingo.DataObjects;
using Otterly.Database.ActivityData.Interfaces;
using Otterly.Database.UserData;

namespace Otterly.API.Handlers.Bingo;

public class BingoGameHandler : IBingoGameHandler
{
	private readonly OtterlyAppsContext _context;
	private readonly IBingoSessionService _sessionService;
	private readonly IPlayerCardDataService _ticketService;

	public BingoGameHandler(OtterlyAppsContext context, 
							IBingoSessionService sessionService, 
							IPlayerCardDataService ticketService)
	{
		_context = context;
		_sessionService = sessionService;
		_ticketService = ticketService;
	}

	public async Task<CreateSessionResponse> CreateSession(Guid userID, int cardID )
	{
		var response = new CreateSessionResponse();
		var user = await _context.OtterlyAppsUsers.FirstOrDefaultAsync(appsUser => appsUser.UserID == userID);
		if (user == null)
		{
			 response.SetError("Unable to find user");
			 return response;
		}

		var card = await _context.BingoCards
								 .Include(bingoCard => bingoCard.Slots)
								 .FirstOrDefaultAsync(bingoCard => 
								bingoCard.CardID == cardID && 
								bingoCard.UserID == userID &&
								!bingoCard.Deleted);
		if (card == null)
		{
			response.SetError("Unable to find appropriate card");
			return response;
		}
		var session = await _sessionService.CreateNewSession(BingoMapper.Map(card), UserMapper.Map(user));

		if (session == null)
		{
			response.SetError("unable to create session");
			return response;
		}
		return new CreateSessionResponse()
		{
			CreatedSession = GameMapper.Map(session)
		};
	}

	public async Task<PlayerTicket?> CreatePlayerTicket(string playerTwitchID, BingoSession session)
	{
		int totalNumberSpots = (int)Math.Pow(session.Size, 2);
		if (session.FreeSpace)
		{
			totalNumberSpots -= 1;
		}

		var randomiser = new Random();
		var randomisedSlots = session.SessionItems.OrderBy(_ => randomiser.Next()).Take(totalNumberSpots).ToList();
		if (session.Id != null)
		{
			var playeritems = GameMapper.Map(randomisedSlots).ToList();
			if (session.FreeSpace)
			{
				playeritems.Insert(randomisedSlots.Count / 2, PlayerTicketItem.CreateFreeSpace(session.Id));
			}


			var ticket = await _ticketService.CreatePlayerTicket(playerTwitchID, session.Id, playeritems);
			session.Meta.NumberTickets++;
			await _sessionService.UpdateAsync(session.Id, session);
			return ticket;
		}

		return null;
	}


	public async Task<BaseResponse> MarkTicketItem(PlayerTicket ticket, int requestItemIndex)
	{
		var response = new BaseResponse();
		if (string.IsNullOrEmpty(ticket.Id))
		{
			response.SetError("Invalid Ticket");
			return response;
		}
		var slot = ticket.Slots.FirstOrDefault(item => item.ItemIndex == requestItemIndex);
		if (slot == null)
		{
			response.SetError("Unable to find item in ticket");
			return response;
		}

		slot.Selected = !slot.Selected;
		await _ticketService.UpdateAsync(ticket.Id, ticket);
		return response;
	}

	public async Task<BaseResponse> VerifySessionItem(BingoSession session, int requestItemIndex, bool requestState)
	{
		var response = new BaseResponse();
		if (string.IsNullOrEmpty(session.Id))
		{
			response.SetError("Invalid Ticket");
			return response;
		}
		var slot = session.SessionItems.FirstOrDefault(item => item.ItemIndex == requestItemIndex);
		if (slot == null)
		{
			response.SetError("Unable to find item in ticket");
			return response;
		}

		slot.Verified = requestState;
		await _sessionService.UpdateAsync(session.Id, session);

		response = await MarkAllSessionTicketItemsVerified(slot);

		return response;
	}

	public async Task<BaseResponse> EndSession(BingoSession session)
	{
		var response = new BaseResponse();
		if (!string.IsNullOrEmpty(session.Id))
		{
			session.Active = false;
			await _sessionService.UpdateAsync(session.Id, session);
			return response;
		}
		response.SetError("Session Not Valid");
		return response;
	}


	private async Task<BaseResponse> MarkAllSessionTicketItemsVerified(BingoSessionItem markedItem)
	{
		var response = new BaseResponse();
		try
		{

			var tickets = await _ticketService.GetAllTicketsForSession(markedItem.SessionID);
			if (tickets.Any())
			{
				tickets.ForEach(ticket =>
				{
					var slot = ticket.Slots.FirstOrDefault(item => item.ItemIndex == markedItem.ItemIndex);
					if (slot != null)
					{
						slot.Verified = markedItem.Verified;
					}
				});

				await _ticketService.UpdateListAsync(tickets);
			}
		}
		catch (Exception e)
		{
			response.SetError(e.Message);
		}


		return response;
	}

	#region SimpleGet

	public Task<PlayerTicket?> GetLatestCardData(string cardID) => _ticketService.GetTicketByID(cardID);

	public Task<BingoSession?> GetCurrentSessionForStreamer(string streamerTwitchID) => _sessionService.FindActiveSessionForStreamer(streamerTwitchID);
	public async Task<BingoSessionDTO?> GetCurrentSessionForUser(Guid userID)
	{
		var session = await _sessionService.FindActiveSessionForUser(userID);
		return session != null ? GameMapper.Map(session) : null;
	}

	public Task<PlayerTicket?> GetTicketForPlayer(string playerTwitchID, string sessionID) => _ticketService.FindTicket(playerTwitchID, sessionID);
	public Task<BingoSession?> GetSessionData(string requestSessionID) => _sessionService.GetAsync(requestSessionID);

	public async Task<BingoSessionMeta?> GetCurrentSessionMeta(Guid userID) { 		
		var session = await _sessionService.FindActiveSessionForUser(userID);

		return session?.Meta;
	}

	#endregion
}