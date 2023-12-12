using System;
using System.Threading.Tasks;
using LDSoft.APIClient;
using Otterly.API.ClientLib.Messages.Bingo;
using Otterly.API.ClientLib.Objects.Bingo;
using Otterly.Database.ActivityData.Bingo.DataObjects;

namespace Otterly.API.Handlers.Interfaces;

public interface IBingoGameHandler
{
    Task<CreateSessionResponse> CreateSession(Guid userID, int cardID);
    Task<PlayerTicket?> CreatePlayerTicket(StreamerTicketRequest playerTwitchID, BingoSession sessionID);
    Task<PlayerTicket?> GetLatestCardData(string cardID);
	Task<BingoSession?> GetCurrentSessionForStreamer(string streamerTwitchID);
	Task<BingoSessionDTO?> GetCurrentSessionForUser(Guid userID);
	Task<PlayerTicket?> GetTicketForPlayer(string playerTwitchID, string sessionID);
	Task<BaseResponse> MarkTicketItem(PlayerTicket ticket, MarkItemRequest requestItemIndex);
	Task<BingoSession?> GetSessionData(string requestSessionID);
	Task<BaseResponse> VerifySessionItem(BingoSession session, VerifyItemRequest request);
	Task<BaseResponse> EndSession(BingoSession session);
	Task<BingoSessionMeta?> GetCurrentSessionMeta(Guid userID);
	Task<GetVerificationQueueResponse> GetVerificationQueueForUser(string sessionID);
}