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
			return details == null ? NotFound() : Ok(details);
		}

		[HttpPost]
		public async Task<IActionResult> UpdateCardDetails(UpdateCardDetailsRequest request)
		{
			var details = await _cardHandler.UpdateCardDetails( request.UserID, request.CardDetails);
			return details == null
					   ? StatusCode(500, new BaseResponse("Unable to find card to update"))
					   : Ok(new GetCardDetailsResponse{Card = details});
		}
		[HttpPut]
		public async Task<IActionResult> AddNewCard(UpdateCardDetailsRequest request)
		{
			var details = await _cardHandler.AddNewCard( request.UserID, request.CardDetails);
			return details == null
					   ? StatusCode(500, new BaseResponse("Unable To Add Card"))
					   : Ok(new GetCardDetailsResponse{Card = details});
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteCard(UpdateCardDetailsRequest request)
		{
			var success = await _cardHandler.DeleteCard( request.UserID, request.CardDetails);
			return success
					   ? Ok()
					   : StatusCode(500, new BaseResponse("Unable To Delete Card"));
		}
	}
}
