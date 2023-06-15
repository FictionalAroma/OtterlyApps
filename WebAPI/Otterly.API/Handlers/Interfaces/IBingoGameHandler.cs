using System;
using System.Threading.Tasks;
using Otterly.ClientLib;
using Otterly.Database.ActivityData.Bingo.DataObjects;

namespace Otterly.API.Handlers.Interfaces;

public interface IBingoGameHandler
{
    Task<BaseResponse> CreateSession(Guid userID, int cardID);
    Task<BaseResponse> CreatePlayerTicket(Guid playerTwitchID, Guid sessionID);
    Task<PlayerTicket> GetLatestCardData(Guid ticketID);
    Task<BingoSession> GetCurrentSessionForStreamer(Guid streamerTwitchID);
}