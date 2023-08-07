using System;
using System.Threading.Tasks;
using LDSoft.APIClient;
using Otterly.API.ClientLib;
using Otterly.API.ClientLib.Bingo;
using Otterly.API.DataObjects.Bingo;
using Otterly.Database.ActivityData.Bingo.DataObjects;

namespace Otterly.API.Handlers.Interfaces;

public interface IBingoGameHandler
{
    Task<CreateSessionResponse> CreateSession(Guid userID, int cardID);
    Task<PlayerTicket?> CreatePlayerTicket(string playerTwitchID, BingoSession sessionID);
    Task<PlayerTicket?> GetLatestCardData(string cardID);
	Task<BingoSession?> GetCurrentSessionForStreamer(string streamerTwitchID);
	Task<BingoSessionDTO?> GetCurrentSessionForUser(Guid userID);
	Task<PlayerTicket?> GetTicketForPlayer(string playerTwitchID, string sessionID);
	Task<BaseResponse> MarkTicketItem(PlayerTicket ticket, int requestItemIndex);
	Task<BingoSession?> GetSessionData(string requestSessionID);
	Task<BaseResponse> VerifySessionItem(BingoSession session, int requestItemIndex, bool requestState);
	Task<BaseResponse> EndSession(BingoSession session);
	Task<BingoSessionMeta?> GetCurrentSessionMeta(Guid userID);
}