using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Otterly.API.ClientLib;
using Otterly.API.ClientLib.Bingo;
using Otterly.API.DataObjects.Bingo;
using Otterly.API.Handlers.Interfaces;
using Otterly.Database.UserData;

namespace Otterly.API.Handlers.Bingo;

public class CardHandler : ICardHandler
{
    private readonly OtterlyAppsContext _context;
    private readonly IMapper _mapper;
    public CardHandler(OtterlyAppsContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<BingoCardDTO>> GetCardsForUser(Guid userID)
    {
        var card = await _context.BingoCards.
                                  Where(card => card.UserID == userID && !card.Deleted)
                                  .Include(bingoCard => bingoCard.Slots)
                                  .ToListAsync();

        return _mapper.Map<List<BingoCardDTO>>(card);
    }

    public async Task<GetCardDetailsResponse> GetCardDetail(int cardID)
    {
        var foundCard = await _context.BingoCards.FindAsync(cardID);
        if (foundCard == null) return null;

        await _context.Entry(foundCard).Collection(card => card.Slots).LoadAsync();
        var response = new GetCardDetailsResponse()
        {
            Card = _mapper.Map<BingoCardDTO>(foundCard),
        };

        return response;
    }

    public async Task<BaseResponse> UpdateCardDetails(UpdateCardDetailsRequest request)
    {
        var response = new BaseResponse();

        var foundCard = await _context.BingoCards.
                                             Where(card => card.CardID == request.CardDetails.CardID)
                                             .Include(bingoCard => bingoCard.Slots)
                                             .FirstOrDefaultAsync();
        if (foundCard == null)
        {
            response.SetError("Unable to find card !?");
            return response;
        }

        _mapper.Map(request.CardDetails, foundCard);
        await _context.SaveChangesAsync();


        return response;
    }

}