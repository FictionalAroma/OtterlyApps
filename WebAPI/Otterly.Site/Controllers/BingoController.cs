using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Http;
using Otterly.API.ClientLib;

namespace Otterly.Site.Controllers
{
	[Authorize]
	[Controller]
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
    }
}
