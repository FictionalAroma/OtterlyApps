using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
                                  Where(card => card.UserID == userID && !card.Deleted)
                                  .Include(bingoCard => bingoCard.Slots)
                                  .ToListAsync();

        return BingoMapper.Map(card);
    }

    public async Task<BingoCardDTO?> GetCardDetail(int cardID, Guid requestUserID)
	{
		var foundCard = await _context.BingoCards
									  .Include(card => card.Slots)
									  .FirstOrDefaultAsync(card => card.CardID == cardID &&
																   card.UserID == requestUserID &&
																   !card.Deleted);
        return foundCard == null ? null : BingoMapper.Map(foundCard);
	}

    public async Task<BingoCardDTO?> UpdateCardDetails(Guid requestUserID, BingoCardDTO cardDTO)
    {
        var response = new GetCardDetailsResponse();


        var foundCard = await FindCard(requestUserID, cardDTO);
        if (foundCard == null)
		{
			return null;
		}

		foundCard.CardName = cardDTO.CardName;
		foundCard.TitleText = cardDTO.TitleText;
		foundCard.CardSize = cardDTO.CardSize;
		foundCard.FreeSpace = cardDTO.FreeSpace;

		foreach (var dto in cardDTO.Slots)
		{
			var slot = foundCard.Slots.FirstOrDefault(slot => dto.SlotIndex == slot.SlotIndex);
			if (slot != null)
			{
				slot = BingoMapper.Map(dto);
			}
			else
			{
				var newSlot = BingoMapper.Map(dto);
				_context.BingoSlots.Add(newSlot);
                foundCard.Slots.Add(newSlot);
			}
		}

		await _context.SaveChangesAsync();
		await _context.Entry(foundCard).ReloadAsync();

		var endResultCard = BingoMapper.Map(foundCard);
        await _context.SaveChangesAsync();


        return endResultCard;
    }

	private async Task<BingoCard?> FindCard(Guid requestUserID, BingoCardDTO cardDTO)
	{
		return await _context.BingoCards.
							  Where(card => card.CardID == cardDTO.CardID && 
											card.UserID == requestUserID)
							  .Include(bingoCard => bingoCard.Slots)
							  .FirstOrDefaultAsync();
	}

	public async Task<BingoCardDTO?> AddNewCard(Guid requestUserID, BingoCardDTO card)
	{
		if (card.CardID != null)
		{
			return null;
		}

		var cardToInsert = BingoMapper.Map(card);
		cardToInsert.UserID = requestUserID;
		_context.BingoCards.Add(cardToInsert);
		await _context.SaveChangesAsync();
		await _context.Entry(cardToInsert).ReloadAsync();

		return BingoMapper.Map(cardToInsert);

	}

	public async Task<bool> DeleteCard(Guid requestUserID, BingoCardDTO cardDTO)
	{
		var foundCard = await FindCard(requestUserID, cardDTO);
		if (foundCard == null)
		{
			return false;
		}

		_context.BingoCards.Remove(foundCard);
		await _context.SaveChangesAsync();
		return true;
	}
}