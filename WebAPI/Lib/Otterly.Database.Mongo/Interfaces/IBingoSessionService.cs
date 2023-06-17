using System;
using System.Threading.Tasks;
using Otterly.API.DataObjects.Bingo;
using Otterly.ClientLib;
using Otterly.Database.ActivityData.Bingo.DataObjects;

namespace Otterly.Database.ActivityData.Interfaces;

public interface IBingoSessionService : IMongoServiceBase<BingoSession>
{
    Task<BaseResponse> CreateNewSession(BingoCardDTO card, OtterlyAppsUserDTO user);
	Task<BingoSession?> FindActiveSessionForStreamer(Guid streamerTwitchID);
}