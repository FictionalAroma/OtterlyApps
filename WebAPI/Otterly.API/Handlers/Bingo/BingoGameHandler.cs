using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Otterly.ClientLib;
using Otterly.Database;

namespace Otterly.API.Handlers.Bingo;

public class BingoGameHandler
{
	private readonly OtterlyAppsContext _context;
	public BingoGameHandler(OtterlyAppsContext context) { _context = context; }

	public async Task<BaseResponse> CreateSession(Guid userID, int cardID )
	{
		var card = await _context.BingoCards.FirstOrDefaultAsync(bingoCard => 
																	 bingoCard.CardID == cardID && 
																	 bingoCard.UserID == userID &&
																	 !bingoCard.Deleted);
		if (card == null)
		{
			return new BaseResponse("Unable to find appropriate card");
		}




		return default;
	}
}