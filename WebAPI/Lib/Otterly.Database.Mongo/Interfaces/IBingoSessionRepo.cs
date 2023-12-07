using System;
using System.Threading.Tasks;
using Otterly.API.ClientLib.Objects.Bingo;
using Otterly.API.ClientLib.Objects.User;
using Otterly.Database.ActivityData.Bingo.DataObjects;

namespace Otterly.Database.ActivityData.Interfaces;

public interface IBingoSessionRepo
{
    Task<BingoSession?> CreateNewSession(BingoCardDTO card, OtterlyAppsUserDTO user);
	Task<BingoSession?> FindActiveSessionForStreamer(string streamerTwitchID);
	Task<BingoSession?> FindActiveSessionForUser(Guid userID);
	Task UpdateAsync(BingoSession session);
	Task<BingoSession?> GetByID(string requestSessionID);
}