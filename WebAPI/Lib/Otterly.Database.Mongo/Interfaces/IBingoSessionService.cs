using System;
using System.Threading.Tasks;
using Otterly.API.ClientLib;
using Otterly.API.DataObjects.Bingo;
using Otterly.API.DataObjects.User;
using Otterly.Database.ActivityData.Bingo.DataObjects;

namespace Otterly.Database.ActivityData.Interfaces;

public interface IBingoSessionService : IMongoServiceBase<BingoSession>
{
    Task<BaseResponse> CreateNewSession(BingoCardDTO card, OtterlyAppsUserDTO user);
	Task<BingoSession?> FindActiveSessionForStreamer(string streamerTwitchID);
}