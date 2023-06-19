using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Otterly.API.ClientLib;

namespace Otterly.Site.StartupExtensions;

public static class APIClientStartup
{
	public static WebApplicationBuilder AddOtterlyAPIClient(this WebApplicationBuilder builder)
	{

		builder.Services.AddHttpClient<OtterlyAPIClient>();

		return builder;
	}
}