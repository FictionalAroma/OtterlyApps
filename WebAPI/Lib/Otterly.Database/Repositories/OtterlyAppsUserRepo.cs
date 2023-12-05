using Microsoft.EntityFrameworkCore;
using Otterly.Database.UserData.DataObjects;
using Otterly.Database.UserData.Interfaces;

namespace Otterly.Database.UserData.Repositories;

public class OtterlyAppsUserRepo : BaseRepo, IOtterlyAppsUserRepo
{

	public async Task<OtterlyAppsUser?> GetUser(Guid userID) => 
		await Context.OtterlyAppsUsers.FindAsync(userID);

	public async Task<OtterlyAppsUser?> GetUserByTwitchID(string newUserTwitchID) =>
		await Context.OtterlyAppsUsers.FirstOrDefaultAsync(user => 
			user.TwitchID == newUserTwitchID);

	public async Task AddAsync(OtterlyAppsUser user) => 
		await Context.OtterlyAppsUsers.AddAsync(user); 
}