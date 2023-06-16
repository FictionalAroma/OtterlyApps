using System;
using System.Threading.Tasks;
using Otterly.API.ClientLib.Bingo;
using Otterly.ClientLib;
using Otterly.Database.ActivityData.Bingo.DataObjects;

namespace Otterly.API.Handlers.Interfaces;

public interface IBingoGameHandler
{
    Task<BaseResponse> CreateSession(Guid userID, int cardID);
    Task<CreateTicketResponse> CreatePlayerTicket(Guid playerTwitchID, BingoSession sessionID);
    Task<PlayerTicket?> GetLatestCardData(Guid ticketID);
    Task<BingoSession?> GetCurrentSessionForStreamer(Guid streamerTwitchID);
}