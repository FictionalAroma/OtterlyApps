using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Otterly.API.ClientLib.Bingo;
using Otterly.API.Handlers.Interfaces;

namespace Otterly.API.Controllers.Bingo
{
	[Authorize]
    [Route("api/bingo/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
	{
		// ReSharper disable once InconsistentNaming
		private static readonly Guid TEST_USER = new Guid("e3aeefa0-b2df-4af7-9033-914ef6936bf0");

		private readonly ICardHandler _cardHandler;

		public CardController(ICardHandler cardHandler) { _cardHandler = cardHandler; }

		[HttpGet]
		public async Task<IActionResult> GetCards()
		{
			var results = await _cardHandler.GetCardsForUser(TEST_USER);
			return Ok(results);
		}

		[HttpGet]
		[Route("Details")]
		public async Task<IActionResult> GetCardDetail(int cardID)
		{
			var details = await _cardHandler.GetCardDetail(cardID);
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
