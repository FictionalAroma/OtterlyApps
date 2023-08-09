using LDSoft.APIClient;
using LDSoft.AWS;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
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

	public static WebApplicationBuilder ConfigureAWS(this WebApplicationBuilder builder)
	{
		var options = builder.Configuration.GenerateAWSOptionsWithCreds();
		builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
		builder.Services.AddScoped(_ => options);

		builder.Host.ConfigureAppConfiguration(((_, configurationBuilder) =>
												   {
													   configurationBuilder.AddAmazonSecretsManager(options, "eu-west-1", "Otterly/API/Config");
												   }));

		return builder;
	}

}