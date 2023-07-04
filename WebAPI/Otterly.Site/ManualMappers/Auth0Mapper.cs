using System;
using System.Linq;
using Auth0.ManagementApi.Models;
using Otterly.API.DataObjects.User;

namespace Otterly.Site.ManualMappers;

public static class Auth0Mapper
{
	public static OtterlyAppsUserDTO Map(User newAuth0User)
	{
		var newAPIUser = new OtterlyAppsUserDTO()
						 {
							 ProfileImagePath = newAuth0User.Picture,
							 UserName = newAuth0User.UserName,
						 };
		var twichIdent = newAuth0User.Identities.FirstOrDefault(identity =>
																	 string.Equals(identity.Connection, "twitch",
																	  StringComparison.InvariantCultureIgnoreCase));
		if (twichIdent != null && !string.IsNullOrWhiteSpace(twichIdent.UserId))
		{
			newAPIUser.TwitchID = twichIdent.UserId
											.Split('|',
												   StringSplitOptions.RemoveEmptyEntries |
												   StringSplitOptions.TrimEntries)
											.Last();
		}

		return newAPIUser;
	}
}