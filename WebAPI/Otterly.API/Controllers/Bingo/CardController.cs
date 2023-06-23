using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Otterly.API.ClientLib.Bingo;
using Otterly.API.Handlers.Interfaces;
using Otterly.API.ClientLib;

namespace Otterly.API.Controllers.Bingo
{
	[Authorize]
    [Route("api/bingo/[controller]")]
    [ApiController]
	public class CardController : ControllerBase
	{
		// ReSharper disable once InconsistentNaming

		private readonly ICardHandler _cardHandler;

		public CardController(ICardHandler cardHandler) { _cardHandler = cardHandler; }

		[HttpGet]
		public async Task<IActionResult> GetCards(BaseRequest request)
		{
			var results = await _cardHandler.GetCardsForUser(request.UserID);

			return Ok(results);
		}

		[HttpGet]
		[Route("Details")]
		public async Task<IActionResult> GetCardDetail(GetCardDeatilsRequest request)
		{
			var details = await _cardHandler.GetCardDetail(request.CardID, request.UserID);
			if (details == null) return NotFound();

			return Ok(details);
		}

		[HttpPost]
		public async Task<IActionResult> UpdateCardDetails(UpdateCardDetailsRequest request)
		{
			return Ok(await _cardHandler.UpdateCardDetails(request));
		}

	}
}
