using LDSoft.APIClient;
using LDSoft.AWS;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Otterly.API.ClientLib;
using Otterly.Site.Configuration;

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

	public static WebApplicationBuilder AddClientAppConfig(this WebApplicationBuilder builder)
	{
		builder.Services.AddSingleton<ClientAppConfig>(provider => builder.Configuration.GetSection("ClientAppConfig").Get<ClientAppConfig>());

		return builder;
	}

	public static WebApplicationBuilder ConfigureAWS(this WebApplicationBuilder builder)
	{
		var options = builder.Configuration.GenerateAWSOptionsWithCreds();
		if (options == null) return builder;
		builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
		builder.Services.AddScoped(_ => options);


		builder.Host.ConfigureAppConfiguration((_, configurationBuilder) =>
												   {
													   var secrets = builder
																	 .Configuration.GetSection("AWSConfig:SecretNames")
																	 .Get<string[]>();
													   foreach (var s in secrets)
													   {
														   configurationBuilder.AddAmazonSecretsManager(options,
															options.Region.SystemName, s);
													   }
												   });

		return builder;
	}

}