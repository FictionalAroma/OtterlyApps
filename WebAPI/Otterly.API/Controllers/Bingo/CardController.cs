using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Otterly.Database;
using Otterly.Database.DataObjects;

namespace Otterly.API.Controllers.Bingo
{
	//[Authorize]
    [Route("api/bingo/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
	{
        private readonly OtterlyAppsContext _context;

		public CardController(OtterlyAppsContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<List<BingoCard>> GetCards()
		{
			return await _context.BingoCards.ToListAsync();
		}
	}
}
