using System.Text.Json;
using Amazon.Extensions.NETCore.Setup;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
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
		var secret = GetSecret();

		
		Data = JsonSerializer.Deserialize<Dictionary<string, string>>(secret);
	}

	private string GetSecret()
	{
		var request = new GetSecretValueRequest
					  {
						  SecretId = _secretName,
						  VersionStage = "AWSCURRENT" // VersionStage defaults to AWSCURRENT if unspecified.
					  };

		var client = _awsOptions.CreateServiceClient<IAmazonSecretsManager>();
		{
			try
			{


				var response = client.GetSecretValueAsync(request).Result;
				
				string secretString;
				if (response.SecretString != null)
				{
					secretString = response.SecretString;
				}
				else
				{
					var memoryStream = response.SecretBinary;
					var reader = new StreamReader(memoryStream);
					secretString =
						System.Text.Encoding.UTF8
							  .GetString(Convert.FromBase64String(reader.ReadToEnd()));
				}
				return secretString;

			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			return string.Empty;

		}
	}
}