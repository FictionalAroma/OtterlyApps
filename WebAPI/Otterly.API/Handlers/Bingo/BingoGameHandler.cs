using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI;
using Otterly.API.ClientLib.DTO;
using Otterly.API.Handlers.Interfaces;
using Otterly.ClientLib;
using Otterly.Database;
using Otterly.Database.ActivityData.Bingo.DataObjects;
using Otterly.Database.ActivityData.Interfaces;

namespace Otterly.API.Handlers.Bingo;

public class BingoGameHandler : IBingoGameHandler
{
	private readonly OtterlyAppsContext _context;
	private readonly IMapper _mapper;
	private readonly IBingoSessionService _sessionService;
	private readonly IPlayerCardDataService _ticketService;
	public BingoGameHandler(OtterlyAppsContext context, IMapper mapper, IBingoSessionService sessionService, IPlayerCardDataService ticketService)
	{
		_context = context;
		_mapper = mapper;
		_sessionService = sessionService;
		_ticketService = ticketService;
	}

	public async Task<BaseResponse> CreateSession(Guid userID, int cardID )
	{
		var user = await _context.OtterlyAppsUsers.FirstOrDefaultAsync(appsUser => appsUser.UserID == userID);
		if (user == null)
		{
			return new BaseResponse("Unable to find user");
		}

		var card = await _context.BingoCards
								 .Include(bingoCard => bingoCard.Slots)
								 .FirstOrDefaultAsync(bingoCard => 
								bingoCard.CardID == cardID && 
								bingoCard.UserID == userID &&
								!bingoCard.Deleted);
		if (card == null)
		{
			return new BaseResponse("Unable to find appropriate card");
		}
		
		return await _sessionService.CreateNewSession(_mapper.Map<BingoCardDTO>(card), _mapper.Map<OtterlyAppsUserDTO>(user));;
	}

	public async Task<BaseResponse> CreatePlayerTicket(Guid playerTwitchID, Guid sessionID)
	{
		return default;
	}

	public async Task<PlayerTicket> GetLatestCardData(Guid ticketID)
	{
		return default;
	}

	public async Task<BingoSession> GetCurrentSessionForStreamer(Guid streamerTwitchID)
	{
		return default;
	}
}