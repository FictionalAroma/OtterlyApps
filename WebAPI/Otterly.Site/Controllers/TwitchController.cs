using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TwitchLib.Api.Auth;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.HttpCallHandlers;
using TwitchLib.Api.Core.RateLimiter;

namespace Otterly.Site.Controllers
{
    [Route("bff/twitch")]
    [ApiController]
    public class TwitchController : ControllerBase
    {
		private readonly Auth _auth;

		public TwitchController(IConfiguration config, ILogger<TwitchHttpClient> twitchLogger)
		{

			//var settings = new ApiSettings() {ClientId = };
			//_auth = new Auth(settings, new BypassLimiter(), new TwitchHttpClient(twitchLogger));
		}

		public string GetTwitchAuthLink() { return ""; }
	}
}
