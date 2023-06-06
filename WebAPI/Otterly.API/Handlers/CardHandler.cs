using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Otterly.API.Handlers.Interfaces;
using Otterly.ClientLib.Bingo.DTO;
using Otterly.ClientLib.Bingo.Messaging;
using Otterly.Database;
using Otterly.Database.DataObjects;

namespace Otterly.API.Handlers;

public class CardHandler : ICardHandler
{
	private OtterlyAppsContext _context;
	private IMapper _mapper;
	public CardHandler(OtterlyAppsContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<List<BingoCard>> GetCardsForUser(Guid userID)
	{
		return await _context.BingoCards.Where(card => card.UserID == userID).ToListAsync();
	}

	public async Task<GetCardDetails> GetCardDetail(int cardID)
	{
		var card = await _context.BingoCards.FindAsync(cardID);
		if (card == null) return null;

		var response = new GetCardDetails()
					   {
						   Card = _mapper.Map<BingoCardDTO>(card),
					   };
		await _context.BingoSlots
					  .Where(slot => slot.CardID == cardID)
					  .ForEachAsync(slot =>
										response.CardFields.Add(_mapper.Map<BingoSlotDTO>(slot)));

		return response;
	}


}