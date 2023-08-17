using Amazon.Extensions.NETCore.Setup;
using Microsoft.Extensions.Configuration;

namespace LDSoft.AWS.RDSUtils
{

	public class AmazonSecretsManagerConnectionStringConfigurationSource : IConfigurationSource
	{
		private readonly AWSOptions _awsOptions;
		private readonly string _region;
		private readonly string _secretName;

		public AmazonSecretsManagerConnectionStringConfigurationSource(AWSOptions awsOptions,
																	   string region,
																	   string secretName)
		{
			_awsOptions = awsOptions;
			_region = region;
			_secretName = secretName;
		}

		public IConfigurationProvider Build(IConfigurationBuilder builder)
		{
			return new AmazonSecretsManagerConnectionStringConfigurationProvider(_awsOptions, _region, _secretName);
		}
	}
}