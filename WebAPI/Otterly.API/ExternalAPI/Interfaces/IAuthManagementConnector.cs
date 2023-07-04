using System.Threading.Tasks;
using Otterly.Database.UserData.DataObjects;

namespace Otterly.API.ExternalAPI.Interfaces;

public interface IAuthManagementConnector
{
	Task<bool> OnCreatedUser(OtterlyAppsUser userToUpdate);
}