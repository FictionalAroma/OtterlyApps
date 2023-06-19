using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Otterly.API.ClientLib;

namespace Otterly.Site.Controllers
{
	[Authorize]
	[Controller]
	[Route("bff/[controller]")]
    public class BingoController : ControllerBase
    {
		private readonly OtterlyAPIClient _client;

		public BingoController(OtterlyAPIClient client) { this._client = client; }

		[HttpGet]
		[Route("GetCards")]
		public async Task<IActionResult> GetCards()
		{
			return new JsonResult(await _client.GetCards());
		}
    }
}
