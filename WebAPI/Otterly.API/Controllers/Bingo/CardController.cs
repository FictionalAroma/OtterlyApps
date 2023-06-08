using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Otterly.API.Handlers.Interfaces;

namespace Otterly.API.Controllers.Bingo
{
	[Authorize]
    [Route("api/bingo/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
	{
		private readonly ICardHandler _cardHandler;

		public CardController(ICardHandler cardHandler) { _cardHandler = cardHandler; }

		[HttpGet]
		public async Task<IActionResult> GetCards()
		{
			var results = await _cardHandler.GetCardsForUser(new Guid("e3aeefa0-b2df-4af7-9033-914ef6936bf0"));
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

	}
}
