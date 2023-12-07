using Otterly.API.ClientLib.Objects.User;

namespace Otterly.API.ClientLib.Messages.Account;

public class CreateUserRequest
{
	public string Auth0ID { get; set; }
	public OtterlyAppsUserDTO UserToCreate { get; set; }
}