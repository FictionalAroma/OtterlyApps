using System.Net.Http;
using System.Threading.Tasks;
using Interview.APIClients.Clients;
using Otterly.API.DataObjects.Bingo;

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