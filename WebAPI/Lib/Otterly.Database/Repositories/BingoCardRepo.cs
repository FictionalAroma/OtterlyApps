using Microsoft.EntityFrameworkCore;
using Otterly.Database.UserData.DataObjects;
using Otterly.Database.UserData.Interfaces;

namespace Otterly.Database.UserData.Repositories;

public class BingoCardRepo : BaseRepo, IBingoCardRepo
{

	public async Task<BingoCard?> GetCardForUser(Guid userID, int cardID, bool includeSlots = true)
	{
		var query = Context.BingoCards;
		if (includeSlots)
		{
			query.Include(bingoCard => bingoCard.Slots);
		}

		return await query.FirstOrDefaultAsync(bingoCard => 
											 bingoCard.CardID == cardID && 
											 bingoCard.UserID == userID &&
											 !bingoCard.Deleted);

	}
}