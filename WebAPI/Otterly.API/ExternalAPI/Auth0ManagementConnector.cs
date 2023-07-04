using System.Collections.Generic;
using System.Threading.Tasks;
using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using Otterly.API.DataObjects.User;
using Otterly.API.ExternalAPI.Interfaces;
using Otterly.Database.UserData.DataObjects;

namespace Otterly.API.ExternalAPI;

public class Auth0ManagementConnector : IAuthManagementConnector
{
	private readonly IManagementApiClient _client;

	public Auth0ManagementConnector(IManagementApiClient client) { _client = client; }

	public async Task<bool> OnCreatedUser(OtterlyAppsUser userToUpdate)
	{
		var externalUser = await _client.Users.GetAsync(userToUpdate.ExternalAuthID);
		if (externalUser.AppMetadata == null)
		{
			externalUser.AppMetadata = new Dictionary<string, string>();
		}
		externalUser.AppMetadata["UUID"] = userToUpdate.UserID.ToString();
		await _client.Users.UpdateAsync(userToUpdate.ExternalAuthID,
										new UserUpdateRequest()
										{
											AppMetadata = externalUser.AppMetadata,
										});
		return true;
	}
}