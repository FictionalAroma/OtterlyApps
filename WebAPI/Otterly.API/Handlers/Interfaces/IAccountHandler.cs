using System;
using System.Threading.Tasks;
using Otterly.API.DataObjects.User;
using Otterly.Database.UserData.DataObjects;

namespace Otterly.API.Handlers.Interfaces;

public interface IAccountHandler
{
	Task<OtterlyAppsUserDTO?> GetUserDTO(Guid userID);
	Task<OtterlyAppsUserDTO?> CreateUser(string newUserAuth0ID, OtterlyAppsUserDTO userID);
	Task<OtterlyAppsUserDTO?> GetUserDTOByTwitchID(string newUserTwitchID);
}