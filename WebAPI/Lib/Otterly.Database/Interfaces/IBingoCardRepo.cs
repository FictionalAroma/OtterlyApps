using Otterly.Database.UserData.DataObjects;

namespace Otterly.Database.UserData.Interfaces;

public interface IBingoCardRepo
{
	Task<BingoCard> GetCardForUser(Guid userID, int cardID, bool includeSlots = true);
}