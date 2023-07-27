using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Http;
using Otterly.API.ClientLib;
using Otterly.API.ClientLib.Bingo;
using Otterly.API.DataObjects.Bingo;

namespace Otterly.Site.Controllers
{
	[ApiController]
	[Route("bff/[controller]/[action]")]
    public class BingoController : APILinkController
    {

		public BingoController(ITypedHttpClientFactory<OtterlyAPIClient> httpClientFactory, HttpClient baseClient) : base(httpClientFactory, baseClient)
		{
		}

		[HttpGet]
		public async Task<IActionResult> GetCards()
		{			
			var client = await GenerateClientAsync();

			var cards = await client.GetCards(UserID);
			return new JsonResult(cards);
		}

		[HttpPost]
		public async Task<IActionResult> UpdateCard(BingoCardDTO cardToUpdate)
		{
			try
			{
				var client = await GenerateClientAsync();

				var result = await client.UpdateCard(cardToUpdate, UserID);
				return result.Success ? new JsonResult(result.Card) : StatusCode(500, result.Error);
			}

			catch (Exception e)
			{
				Console.WriteLine(e);
				return StatusCode(500, e.Message);
			}
		}

		[HttpPut]
		public async Task<IActionResult> AddCard(BingoCardDTO cardToUpdate)
		{
			try
			{
				var client = await GenerateClientAsync();

				var result = await client.AddCard(cardToUpdate, UserID);
				return result.Success ? new JsonResult(result.Card) : StatusCode(500, result.Error);
			}

			catch (Exception e)
			{
				Console.WriteLine(e);
				return StatusCode(500, e.Message);
			}
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteCard(BingoCardDTO cardToUpdate)
		{
			try
			{
				var client = await GenerateClientAsync();

				var result = await client.DeleteCard(cardToUpdate, UserID);
				return result ? Ok(true) : StatusCode(500);
			}

			catch (Exception e)
			{
				Console.WriteLine(e);
				return StatusCode(500, e.Message);
			}
		}

		[HttpGet]
		public async Task<IActionResult> GetCurrentGame()
		{
			try
			{
				var client = await GenerateClientAsync();

				var result = await client.GetCurrentGameSession(UserID);
				return Ok(result);
			}

			catch (Exception e)
			{
				Console.WriteLine(e);
				return StatusCode(500, e.Message);
			}
		}

		[HttpPost]
		public async Task<IActionResult> CreateSession([FromBody]int cardID)
		{
			try
			{
				var client = await GenerateClientAsync();
				var userID = UserID;
				var result = await client.CreateGameSession(UserID, cardID);
				return result.Success ? Ok(result.CreatedSession) : StatusCode(500, result);
			}

			catch (Exception e)
			{
				Console.WriteLine(e);
				return StatusCode(500, e.Message);
			}
		}

		[HttpGet]
		public async Task<IActionResult> EndSession(string sessionID)
		{
			try
			{
				var client = await GenerateClientAsync();

				var result = await client.EndSession(sessionID);
				return Ok(result);
			}

			catch (Exception e)
			{
				Console.WriteLine(e);
				return StatusCode(500, e.Message);
			}
		}

		[HttpPost]
		public async Task<IActionResult> VerifyItem(VerifyItemRequest request)
		{
			try
			{
				var client = await GenerateClientAsync();

				var result = await client.VerifyItem(request);
				return Ok(result);
			}

			catch (Exception e)
			{
				Console.WriteLine(e);
				return StatusCode(500, e.Message);
			}
		}

		[HttpGet]
		public async Task<IActionResult> GetSessionMeta(string sessionID)
		{
			try
			{
				var client = await GenerateClientAsync();

				var result = await client.GetSessionMeta(UserID);
				return Ok(result);
			}

			catch (Exception e)
			{
				Console.WriteLine(e);
				return StatusCode(500, e.Message);
			}
		}

	}
}
