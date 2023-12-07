using System;
using System.Linq;
using System.Threading.Tasks;
using Otterly.API.ClientLib.Objects.User;
using Otterly.API.ExternalAPI.Interfaces;
using Otterly.API.Handlers.Interfaces;
using Otterly.API.ManualMapper;
using Otterly.Database.UserData;
using Otterly.Database.UserData.DataObjects;

namespace Otterly.API.Handlers;

public class AccountHandler : IAccountHandler
{
	private readonly UnitOfWork _context;
	private readonly IAuthManagementConnector _authConnector;

	public AccountHandler(UnitOfWork context, IAuthManagementConnector authConnector)
    {
		_context = context;
		_authConnector = authConnector;
	}

    public async Task<OtterlyAppsUserDTO?> GetUserDTO(Guid userID)
    {
        var user = await _context.UserRepo.GetUser(userID);
		return user != null ? UserMapper.MapToDTO(user) : null;
    }


	public async Task<OtterlyAppsUserDTO?> GetUserDTOByTwitchID(string newUserTwitchID) {         
		var user = await GetUserProfileByTwitchID(newUserTwitchID);
		return user != null ? UserMapper.MapToDTO(user) : null;
	}

	public Task<OtterlyAppsUser?> GetUserProfile(Guid userID) => _context.UserRepo.GetUser(userID);//_context.OtterlyAppsUsers.FindAsync(userID);

	public Task<OtterlyAppsUser?> GetUserProfileByTwitchID(string newUserTwitchID) => _context.UserRepo.GetUserByTwitchID(newUserTwitchID);//_context.OtterlyAppsUsers.FirstOrDefaultAsync(user => user.TwitchID == newUserTwitchID);

	public async Task<OtterlyAppsUserDTO?> CreateUser(string newUserAuth0ID, OtterlyAppsUserDTO newUser)
	{
		if (newUser.UserID != Guid.Empty)
		{
			return null;
		}

		var existingUser = await _context.UserRepo.GetUser(newUser.UserID) ?? await _context.UserRepo.GetUserByTwitchID(newUser.TwitchID);

		if (existingUser == null)
		{
			OtterlyAppsUser user = UserMapper.MapFromDTO(newUser);
			user.UserID = Guid.NewGuid();
			user.ExternalAuthID = newUserAuth0ID;

			await _context.UserRepo.AddAsync(user);
			await _context.SaveChangesAsync();
			existingUser = user;

			await _authConnector.OnCreatedUser(user);
		}
		return UserMapper.MapToDTO(existingUser);
    }


	public async Task<UserAuth?> GetUserAuth(Guid userID, UserAuth.AuthenticationType type)
	{
		var user = await _context.UserRepo.GetUser(userID);
		return user?.AuthList.FirstOrDefault(auth => auth.AuthType == type);
	}
}

