using System.Text.Json;
using Amazon.Extensions.NETCore.Setup;
using LDSoft.AWS.ClientWrappers;
using Microsoft.Extensions.Configuration;

public class AmazonSecretsManagerConfigurationProvider : ConfigurationProvider
{
	private readonly AWSOptions _awsOptions;
	private readonly string _region;
	private readonly string _secretName;
    
	public AmazonSecretsManagerConfigurationProvider(AWSOptions awsOptions, string region, string secretName)
	{
		_awsOptions = awsOptions;
		_region = region;
		_secretName = secretName;
	}
    public override void Load()
    {
		var secret = SecretManagerAPI.GetSecret(_awsOptions, _secretName);

		
		Data = JsonSerializer.Deserialize<Dictionary<string, string>>(secret);
	}

	
}