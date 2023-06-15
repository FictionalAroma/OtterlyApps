using System.Threading.Tasks;
using Otterly.API.ClientLib.DTO;
using Otterly.ClientLib;
using Otterly.Database.ActivityData.Bingo.DataObjects;

namespace Otterly.Database.ActivityData.Interfaces;

public interface IBingoSessionService : IMongoServiceBase<BingoSession>
{
    Task<BaseResponse> CreateNewSession(BingoCardDTO card, OtterlyAppsUserDTO user);
}