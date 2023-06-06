using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Otterly.ClientLib.Bingo.DTO;
using Otterly.ClientLib.Bingo.Messaging;
using Otterly.Database;

namespace Otterly.API.Controllers.Bingo
{
	//[Authorize]
    [Route("api/bingo/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
	{
        private readonly OtterlyAppsContext _context;
		private readonly IMapper _mapper;

		public CardController(OtterlyAppsContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<IActionResult> GetCards()
		{
			return Ok(await _context.BingoCards.ToListAsync());
		}

		[HttpGet]
		[Route("Details")]
		public async Task<IActionResult> GetCardDetail(int cardID)
		{
			var card = await _context.BingoCards.FindAsync(cardID);
			if (card == null) return NotFound();

			var response = new GetCardDetails()
						   {
							   Card = _mapper.Map<BingoCardDTO>(card),
						   };
			await _context.BingoSlots
						  .Where(slot => slot.CardID == cardID)
						  .ForEachAsync(slot =>
											response.CardFields.Add(_mapper.Map<BingoSlotDTO>(slot)));

			return Ok(response);
		}

	}
}
