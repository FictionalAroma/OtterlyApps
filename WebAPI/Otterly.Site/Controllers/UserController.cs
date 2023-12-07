using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Http;
using Otterly.API.ClientLib;

namespace Otterly.Site.Controllers
{
	[Authorize]
	[ApiController]
	[Route("bff/[controller]/[action]")]

    public class UserController : APILinkController
    {
		public UserController(ITypedHttpClientFactory<OtterlyAPIClient> httpClientFactory, HttpClient baseClient) : base(httpClientFactory, baseClient)
		{
		}

		[HttpGet]
		public async Task<IActionResult> Profile()
		{
			try
			{
				var client = await GenerateClientAsync();

				var result = await client.GetUserProfile(UserID);
				return result != null ? new JsonResult(result) : StatusCode(500);
			}

			catch (Exception e)
			{
				Console.WriteLine(e);
				return StatusCode(500, e.Message);
			}
		}

	}
}
