using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Otterly.API.ClientLib;

namespace Otterly.Site.StartupExtensions;

public static class APIClientStartup
{
	public static WebApplicationBuilder AddOtterlyAPIClient(this WebApplicationBuilder builder)
	{
		builder.Services.AddSingleton<APIClientConfig>(provider =>
		{
			var section = builder.Configuration.GetSection("OtterlyAPIConfig");
			var config = new APIClientConfig()
						 {
							 BaseURL = section["BaseURL"],
							 AuthName = section["AuthName"]
						 };

			return config;
		});
		builder.Services.AddHttpClient<OtterlyAPIClient>();

		return builder;
	}
}