using Interview.APIClients.Clients;
using Otterly.API.ClientLib.DTO;

namespace Otterly.ClientLib;

public class OtterlyAPIClient : APIClientBase
{
    public OtterlyAPIClient(HttpClient client) : base(client)
    {

    }


	public async Task<OtterlyAppsUserDTO> GetUserProfile()
	{
		return new OtterlyAppsUserDTO();
	}
}