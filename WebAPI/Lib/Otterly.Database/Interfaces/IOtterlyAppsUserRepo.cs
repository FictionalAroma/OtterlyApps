using Otterly.Database.UserData.DataObjects;

namespace Otterly.Database.UserData.Interfaces;

public interface IOtterlyAppsUserRepo
{
	Task<OtterlyAppsUser?> GetUser(Guid userID);
	Task<OtterlyAppsUser?> GetUserByTwitchID(string newUserTwitchID);

	Task AddAsync(OtterlyAppsUser user);
}