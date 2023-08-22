using Amazon;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using LDSoft.AWS.RDSUtils;
using Microsoft.Extensions.Configuration;

namespace LDSoft.AWS
{
    public static class HostingExtensions
    {
		public static AWSOptions GenerateAWSOptionsWithCreds(this IConfiguration builder)
		{
			var options = builder.GetAWSOptions();
			options.Logging = new AWSOptions.LoggingSetting()
							  {
								  LogResponses = ResponseLoggingOption.OnError,
								  LogTo = LoggingOptions.Console,
							  };
			var chain = new CredentialProfileStoreChain();
			if (!chain.TryGetAWSCredentials(options.Profile, out var credentials))
				throw new Exception($"Failed to find the {options.Profile} profile");

			options.Credentials = credentials as SSOAWSCredentials;
			
			return options;
		}

		public static void AddAmazonSecretsManager(this IConfigurationBuilder configurationBuilder,
												   AWSOptions awsOptions,
												   string region,
												   string secretName)
		{
		
			var configurationSource = new AmazonSecretsManagerConfigurationSource(awsOptions, region, secretName);

			configurationBuilder.Add(configurationSource);
		}
		public static void AddAmazonSecretsManagerConnectString(this IConfigurationBuilder configurationBuilder,
												   AWSOptions awsOptions,
												   string region,
												   string secretName)
		{
		
			var configurationSource = new AmazonSecretsManagerConnectionStringConfigurationSource(awsOptions, region, secretName);

			configurationBuilder.Add(configurationSource);
		}
	}


}
