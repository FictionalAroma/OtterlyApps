using Otterly.API.DataObjects.Bingo;
using Otterly.Database.UserData.DataObjects;

namespace Otterly.API.ManualMapper;

public class UserMapper
{
	public static OtterlyAppsUserDTO Map(OtterlyAppsUser user)
    {
        return new OtterlyAppsUserDTO
        {
            UserID = user.UserID,
            Test = user.Test,
            TwitchID = user.TwitchID
        };
    }
}