using Otterly.API.DataObjects.User;

namespace Otterly.API.ClientLib.Account;

public class CreateUserRequest
{
	public string Auth0ID { get; set; }
	public OtterlyAppsUserDTO UserToCreate { get; set; }
}