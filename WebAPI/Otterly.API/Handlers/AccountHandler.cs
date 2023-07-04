using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Otterly.API.DataObjects.User;
using Otterly.API.ExternalAPI;
using Otterly.API.ExternalAPI.Interfaces;
using Otterly.API.Handlers.Interfaces;
using Otterly.API.ManualMapper;
using Otterly.Database.UserData;
using Otterly.Database.UserData.DataObjects;

namespace Otterly.API.Handlers;

public class AccountHandler : IAccountHandler
{
    private readonly OtterlyAppsContext _context;
	private readonly IWebHostEnvironment _env;
	private readonly IAuthManagementConnector _authConnector;

	public AccountHandler(OtterlyAppsContext context, IWebHostEnvironment env, IAuthManagementConnector authConnector)
    {
        _context = context;
		_env = env;
		_authConnector = authConnector;
	}

    public async Task<OtterlyAppsUserDTO?> GetUserDTO(Guid userID)
    {
        var user = await GetUserProfile(userID);
		return user != null ? UserMapper.Map(user) : null;
    }


	public async Task<OtterlyAppsUserDTO?> GetUserDTOByTwitchID(string newUserTwitchID) {         
		var user = await GetUserProfileByTwitchID(newUserTwitchID);
		return user != null ? UserMapper.Map(user) : null;
	}

	public async Task<OtterlyAppsUser?> GetUserProfile(Guid userID) => await _context.OtterlyAppsUsers.FindAsync(userID);
	public async Task<OtterlyAppsUser?> GetUserProfileByTwitchID(string newUserTwitchID) => await _context.OtterlyAppsUsers.FirstOrDefaultAsync(user => user.TwitchID == newUserTwitchID);

	public async Task<OtterlyAppsUserDTO?> CreateUser(string newUserAuth0ID, OtterlyAppsUserDTO newUser)
	{
		if (newUser.UserID != Guid.Empty)
		{
			return null;
		}

		var existingUser = await GetUserProfile(newUser.UserID) ?? await GetUserProfileByTwitchID(newUser.TwitchID);

		if (existingUser == null)
		{
			OtterlyAppsUser user = UserMapper.Map(newUser);
			user.UserID = Guid.NewGuid();
			user.ExternalAuthID = newUserAuth0ID;

			await _context.OtterlyAppsUsers.AddAsync(user);
			await _context.SaveChangesAsync();
			existingUser = user;

			await _authConnector.OnCreatedUser(user);
		}
		return UserMapper.Map(existingUser);
    }


	public async Task<UserAuth?> GetUserAuth(Guid userID, UserAuth.AuthenticationType type)
	{
		var user = await _context.OtterlyAppsUsers.FindAsync(userID);
		return user?.AuthList.FirstOrDefault(auth => auth.AuthType == type);
	}
}

