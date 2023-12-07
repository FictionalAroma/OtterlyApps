using System;
using System.Linq;
using System.Threading.Tasks;
using LDSoft.APIClient;
using Microsoft.Extensions.Http;
using Otterly.API.ClientLib.Messages.Bingo;
using Otterly.API.ClientLib.Objects.Bingo;
using Otterly.API.ExternalAPI;
using Otterly.API.Handlers.Interfaces;
using Otterly.API.ManualMapper;
using Otterly.Database.ActivityData.Bingo.DataObjects;
using Otterly.Database.ActivityData.Interfaces;
using Otterly.Database.UserData;
using Otterly.Database.UserData.DataObjects;

namespace Otterly.API.Handlers.Bingo;

public class BingoGameHandler : IBingoGameHandler
{
	private readonly UnitOfWork _context;
	private readonly IBingoSessionRepo _sessionRepo;
	private readonly IPlayerTicketRepo _ticketRepo;
	private readonly ITypedHttpClientFactory<TwitchExtensionAPIConnector> _twitchClientFactory;

	public BingoGameHandler(UnitOfWork context, 
							IBingoSessionRepo sessionRepo, 
							IPlayerTicketRepo ticketRepo,
							ITypedHttpClientFactory<TwitchExtensionAPIConnector> twitchClientFactory)
	{
		_context = context;
		_sessionRepo = sessionRepo;
		_ticketRepo = ticketRepo;
		_twitchClientFactory = twitchClientFactory;
	}

	public async Task<CreateSessionResponse> CreateSession(Guid userID, int cardID )
	{
		var response = new CreateSessionResponse();
		var user = await _context.UserRepo.GetUser(userID);
		if (user == null)
		{
			 response.SetError("Unable to find user");
			 return response;
		}

		var card = await _context.BingoCardRepo.GetCardForUser(userID, cardID);
		if (card == null)
		{
			response.SetError("Unable to find appropriate card");
			return response;
		}
		var session = await _sessionRepo.CreateNewSession(BingoMapper.MapToDTO(card), UserMapper.MapToDTO(user));

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


			var ticket = await _ticketRepo.CreatePlayerTicket(playerTwitchID, session.Id, playeritems);
			session.Meta.NumberTickets++;
			await _sessionRepo.UpdateAsync(session);
			return ticket;
		}

		return null;
	}

	public async Task<BaseResponse> MarkTicketItem(PlayerTicket ticket, MarkItemRequest request)
	{
		var response = new BaseResponse();
		if (string.IsNullOrEmpty(ticket.Id))
		{
			response.SetError("Invalid Ticket");
			return response;
		}
		var slot = ticket.Slots.FirstOrDefault(item => item.ItemIndex == request.ItemIndex);
		if (slot == null)
		{
			response.SetError("Unable to find item in ticket");
			return response;
		}
		var session = await GetSessionData(ticket.SessionID);
		if (session == null)
		{
			response.SetError("Session Invalid");

			return response;
		}


		slot.Selected = true;
		await _ticketRepo.UpdateAsync(ticket);

		var activeVerification = await _context.VerificationRepo.GetActiveVerificationForSessionItem(ticket.SessionID, request.ItemIndex);
		if (activeVerification == null)
		{
				activeVerification = new VerificationQueueItem()
									 {
										 ActivatedDateTime = DateTime.Now,
										 SessionID = ticket.SessionID,
										 ExpiryDateTime = DateTime.Now.AddMinutes(session.VerificationTimeoutMinutes),
										 ItemIndex = request.ItemIndex,
										 UserID = session.UserID,
									 };
				await _context.VerificationRepo.AddVerificationQueueItem(activeVerification);
		}
		var playerVerificationItem =
			activeVerification.PlayerLogs.FirstOrDefault(log => log.ItemIndex == request.ItemIndex &&
																log.PlayerID == request.PlayerTwitchID &&
																log.TicketID == ticket.Id);
		if (playerVerificationItem == null)
		{
			activeVerification.PlayerLogs.Add(new VerificationQueuePlayerLog()
											  {
												  ItemIndex = request.ItemIndex,
												  PlayerID = request.PlayerTwitchID,
												  TicketID = ticket.Id,
												  VerificationID = activeVerification.VerificationID,
											  });
		}

		await _context.SaveChangesAsync();

		return response;
	}

	public async Task<BaseResponse> VerifySessionItem(BingoSession session, VerifyItemRequest request)
	{
		var response = new BaseResponse();
		if (string.IsNullOrEmpty(session.Id))
		{
			response.SetError("Invalid Ticket");
			return response;
		}

		var activeVerification =
			await _context.VerificationRepo.GetActiveVerification(session.Id, request.VerificationID);

		if (activeVerification == null)
		{
			response.SetError("No Item To Set Verification");
			return response;
		}

		activeVerification.Result = request.State;
		activeVerification.VerifiedDateTime = DateTime.Now;
		activeVerification.ExpiryDateTime = DateTime.Now + TimeSpan.FromMinutes(session.VerificationTimeoutMinutes);

		var slot = session.SessionItems.FirstOrDefault(item => item.ItemIndex == request.SessionItemIndex);
		if (slot == null)
		{
			response.SetError("Unable to find item in ticket");
			return response;
		}

		//slot.Verified = requestState;
		await _sessionRepo.UpdateAsync(session);

		response = await MarkAllSessionTicketItemsVerified(activeVerification);

		var twitchAPI = _twitchClientFactory.GetClient();
		//twitchAPI.SendExtensionMessage(, session.TwitchUserID);

		return response;
	}

	public async Task<BaseResponse> EndSession(BingoSession session)
	{
		var response = new BaseResponse();
		if (!string.IsNullOrEmpty(session.Id))
		{
			session.Active = false;
			await _sessionRepo.UpdateAsync(session);
			return response;
		}
		response.SetError("Session Not Valid");
		return response;
	}


	private async Task<BaseResponse> MarkAllSessionTicketItemsVerified(VerificationQueueItem markedItem)
	{
		bool mark = markedItem.Result.GetValueOrDefault();

		var response = new BaseResponse();
		try
		{
			var verifiedPlayerLogs = await _context.VerificationRepo.GetAllTicketsForQueueItem(markedItem.VerificationID);
			var tickets = await _ticketRepo.GetAllTicketsForSession(markedItem.SessionID);
			if (tickets.Any() && verifiedPlayerLogs.Any())
			{
				verifiedPlayerLogs.ForEach(log =>
				{
					var ticket = tickets.FirstOrDefault(playerTicket => playerTicket.TwitchUserID == log.PlayerID && playerTicket.Id == log.TicketID);
					var slot = ticket?.Slots.FirstOrDefault(item => item.ItemIndex == markedItem.ItemIndex);
					if (slot != null)
					{
						slot.Verified = mark;
						slot.Selected = mark;
					}
				});

				await _ticketRepo.UpdateListAsync(tickets);
			}
		}
		catch (Exception e)
		{
			response.SetError(e.Message);
		}


		return response;
	}

	#region SimpleGet

	public Task<PlayerTicket?> GetLatestCardData(string cardID) => _ticketRepo.GetTicketByID(cardID);

	public Task<BingoSession?> GetCurrentSessionForStreamer(string streamerTwitchID) => _sessionRepo.FindActiveSessionForStreamer(streamerTwitchID);
	public async Task<BingoSessionDTO?> GetCurrentSessionForUser(Guid userID)
	{
		var session = await _sessionRepo.FindActiveSessionForUser(userID);
		return session != null ? GameMapper.Map(session) : null;
	}

	public Task<PlayerTicket?> GetTicketForPlayer(string playerTwitchID, string sessionID) => _ticketRepo.FindTicket(playerTwitchID, sessionID);
	public Task<BingoSession?> GetSessionData(string requestSessionID) => _sessionRepo.GetByID(requestSessionID);

	public async Task<BingoSessionMeta?> GetCurrentSessionMeta(Guid userID) { 		
		var session = await _sessionRepo.FindActiveSessionForUser(userID);

		return session?.Meta;
	}

	public async Task<GetVerificationQueueResponse> GetVerificationQueueForUser(string sessionID)
	{
		var verifications = await _context.VerificationRepo.GetNonExpiredVerifications(sessionID);
		return new GetVerificationQueueResponse
		{
			Verifications = verifications.ConvertAll(VerificationQueueMapper.MapToDTO),
		};

	}

	#endregion
}