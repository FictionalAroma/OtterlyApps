using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Otterly.Database;
using Otterly.Database.DataObjects;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebFrontend.PageServices
{
    public class BingoPageService : ControllerBase
    {
		private readonly OtterlyAppsContext _context;
		private readonly UserManager<OtterlyAppsUser> _userManager;
		private readonly AuthenticationStateProvider _auth;

		public BingoPageService(OtterlyAppsContext context, UserManager<OtterlyAppsUser> userManager, AuthenticationStateProvider auth)
		{
			_context = context;
			_userManager = userManager;
			_auth = auth;
		}

		public async Task<List<BingoCard>> GetCards()
		{
			var user = await GetUser();

            return await _context.BingoCards.Where(card => card.UserID == user.UserIDParsed).ToListAsync();
		}

		private async Task<OtterlyAppsUser> GetUser()
		{
				var authState = await _auth.GetAuthenticationStateAsync();
				var user = await _userManager.GetUserAsync(authState.User);
				return user;
		}

		public async Task CreateNewCard(string newCardText)
		{
			var user = await GetUser();

            await _context.BingoCards.AddAsync(new BingoCard() { CardName = newCardText, TitleText = newCardText, UserID = user.UserIDParsed});
			await _context.SaveChangesAsync();
		}
	}
}
