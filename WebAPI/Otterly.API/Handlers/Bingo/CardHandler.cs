using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Otterly.API.ClientLib;
using Otterly.API.ClientLib.Bingo;
using Otterly.API.DataObjects.Bingo;
using Otterly.API.Handlers.Interfaces;
using Otterly.API.ManualMapper;
using Otterly.Database.UserData;
using Otterly.Database.UserData.DataObjects;

namespace Otterly.API.Handlers.Bingo;

public class CardHandler : ICardHandler
{
    private readonly OtterlyAppsContext _context;
    public CardHandler(OtterlyAppsContext context)
    {
        _context = context;
    }

    public async Task<List<BingoCardDTO>> GetCardsForUser(Guid userID)
    {
        var card = await _context.BingoCards.
                                  AsNoTracking().
                                  Where(card => card.UserID == userID && !card.Deleted)
                                  .Include(bingoCard => bingoCard.Slots)
                                  .ToListAsync();

        return BingoMapper.Map(card);
    }

    public async Task<GetCardDetailsResponse?> GetCardDetail(int cardID, Guid requestUserID)
	{
		var foundCard = await _context.BingoCards
									  .AsNoTracking()
									  .Include(card => card.Slots)
									  .FirstOrDefaultAsync(card => card.CardID == cardID &&
																  card.UserID == requestUserID &&
																  !card.Deleted);
        if (foundCard == null) return null;

        var response = new GetCardDetailsResponse()
        {
            Card = BingoMapper.Map(foundCard),
        };

        return response;
    }

    public async Task<BaseResponse> UpdateCardDetails(UpdateCardDetailsRequest request)
    {
        var response = new BaseResponse();

        var foundCard = await _context.BingoCards.
                                             Where(card => card.CardID == request.CardDetails.CardID && 
														   card.UserID == request.UserID)
                                             .Include(bingoCard => bingoCard.Slots)
                                             .FirstOrDefaultAsync();
        if (foundCard == null)
        {
            response.SetError("Unable to find card !?");
            return response;
        }

		var bingoCardDTO = request.CardDetails;
		foundCard.CardName = bingoCardDTO.CardName;
		foundCard.TitleText = bingoCardDTO.TitleText;
		foundCard.CardSize = bingoCardDTO.CardSize;
		foundCard.FreeSpace = bingoCardDTO.FreeSpace;

		foreach (var dto in request.CardDetails.Slots)
		{
			var slot = foundCard.Slots.FirstOrDefault(slot => dto.SlotIndex == slot.SlotIndex);
			if (slot != null)
			{
				slot = BingoMapper.Map(dto);
			}
			else
			{
				BingoSlot newSlot = BingoMapper.Map(dto);
				_context.BingoSlots.Add(newSlot);
                foundCard.Slots.Add(newSlot);
			}
		}

		
        await _context.SaveChangesAsync();


        return response;
    }

}