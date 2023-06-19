using System;
using System.Threading.Tasks;
using Otterly.API.ClientLib;
using Otterly.Database.ActivityData.Bingo.DataObjects;

namespace Otterly.API.Handlers.Interfaces;

public interface IBingoGameHandler
{
    Task<BaseResponse> CreateSession(Guid userID, int cardID);
    Task<PlayerTicket?> CreatePlayerTicket(Guid playerTwitchID, BingoSession sessionID);
    Task<PlayerTicket?> GetLatestCardData(string cardID);
	Task<BingoSession?> GetCurrentSessionForStreamer(Guid streamerTwitchID);
	Task<PlayerTicket?> GetTicketForPlayer(Guid playerTwitchID, string sessionID);
	Task<BaseResponse> MarkTicketItem(PlayerTicket ticket, int requestItemIndex);
	Task<BingoSession?> GetSessionData(string requestSessionID);
	Task<BaseResponse> VerifySessionItem(BingoSession session, int requestItemIndex);
	Task<BaseResponse> EndSession(BingoSession session);
}