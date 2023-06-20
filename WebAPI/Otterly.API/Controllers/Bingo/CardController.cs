using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

		private readonly ICardHandler _cardHandler;

		public CardController(ICardHandler cardHandler) { _cardHandler = cardHandler; }

		[HttpGet]
		public async Task<IActionResult> GetCards()
		{
			var userGuid = User.FindFirst("uuid");

			if (userGuid != null)
			{
				var results = await _cardHandler.GetCardsForUser(Guid.Parse(userGuid.Value));
				return Ok(results);
			}

			return BadRequest();
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
