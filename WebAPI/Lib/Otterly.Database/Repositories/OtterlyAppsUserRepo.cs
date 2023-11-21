using Microsoft.EntityFrameworkCore;
using Otterly.Database.UserData.DataObjects;
using Otterly.Database.UserData.Interfaces;

namespace Otterly.Database.UserData.Repositories;

public class OtterlyAppsUserRepo : IOtterlyAppsUserRepo
{
	private readonly OtterlyAppsContext _context;

	public OtterlyAppsUserRepo(OtterlyAppsContext context)
	{
		_context = context;
	}

	public async Task<OtterlyAppsUser?> GetUser(Guid userID) => 
		await _context.OtterlyAppsUsers.FindAsync(userID);

	public async Task<OtterlyAppsUser?> GetUserByTwitchID(string newUserTwitchID) =>
		await _context.OtterlyAppsUsers.FirstOrDefaultAsync(user => 
			user.TwitchID == newUserTwitchID);

	public async Task AddAsync(OtterlyAppsUser user) => 
		await _context.OtterlyAppsUsers.AddAsync(user); 
}