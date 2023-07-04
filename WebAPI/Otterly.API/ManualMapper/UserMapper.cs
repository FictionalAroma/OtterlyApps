using Otterly.API.DataObjects.User;
using Otterly.Database.UserData.DataObjects;

namespace Otterly.API.ManualMapper;

public class UserMapper
{
	public static OtterlyAppsUserDTO Map(OtterlyAppsUser user)
	{
		return new OtterlyAppsUserDTO()
		{
			ProfileImagePath = user.ProfileImagePath,
			TwitchID = user.TwitchID,
			UserID = user.UserID,
			UserName = user.Username,
			EmailAddress = user.EmailAddress,
		};
	}

	public static OtterlyAppsUser Map(OtterlyAppsUserDTO newUser)
    {
        return new OtterlyAppsUser
        {
            UserID = newUser.UserID,
            TwitchID = newUser.TwitchID,
            Username = newUser.UserName,
            EmailAddress = newUser.EmailAddress,
            ProfileImagePath = newUser.ProfileImagePath
        };
    }
}