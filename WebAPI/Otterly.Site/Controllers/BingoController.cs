using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Http;
using Otterly.API.ClientLib;
using Otterly.API.DataObjects.Bingo;

namespace Otterly.Site.Controllers
{
	[ApiController]
	[Route("bff/bingo")]
    public class BingoController : BFFBaseController
    {

		public BingoController(ITypedHttpClientFactory<OtterlyAPIClient> httpClientFactory, HttpClient baseClient) : base(httpClientFactory, baseClient)
		{
		}

		[HttpGet]
		[Route("GetCards")]
		public async Task<IActionResult> GetCards()
		{			
			var client = await GenerateClientAsync();

			var cards = await client.GetCards(UserID);
			return new JsonResult(cards);
		}

		[HttpPost]
		[Route("UpdateCard")]
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
		[Route("AddCard")]
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
		[Route("DeleteCard")]
		public async Task<IActionResult> DeleteCard(BingoCardDTO cardToUpdate)
		{
			try
			{
				var client = await GenerateClientAsync();

				var result = await client.DeleteCard(cardToUpdate, UserID);
				return result ? Ok() : StatusCode(500);
			}

			catch (Exception e)
			{
				Console.WriteLine(e);
				return StatusCode(500, e.Message);
			}
		}


    }
}
