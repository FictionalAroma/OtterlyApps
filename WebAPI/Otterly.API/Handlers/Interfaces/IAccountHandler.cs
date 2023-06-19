using System;
using System.Threading.Tasks;
using Otterly.API.DataObjects.Bingo;
using Otterly.Database.UserData.DataObjects;

namespace Otterly.API.Handlers.Interfaces;

public interface IAccountHandler
{
	Task<OtterlyAppsUserDTO> GetUserProfile(Guid userID);
	Task<OtterlyAppsUser> CreateUser(Guid userID);
}