using Amazon.Extensions.NETCore.Setup;
using Microsoft.Extensions.Configuration;

public class AmazonSecretsManagerConfigurationSource : IConfigurationSource
{
	private readonly AWSOptions _awsOptions;
	private readonly string _region;
	private readonly string _secretName;

	public AmazonSecretsManagerConfigurationSource(AWSOptions awsOptions, string region, string secretName)
	{
		_awsOptions = awsOptions;
		_region = region;
		_secretName = secretName;
	}

	public IConfigurationProvider Build(IConfigurationBuilder builder)
	{
		return new AmazonSecretsManagerConfigurationProvider(_awsOptions, _region, _secretName);
	}
}