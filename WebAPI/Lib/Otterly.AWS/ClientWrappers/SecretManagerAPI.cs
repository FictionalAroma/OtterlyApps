using Amazon.Extensions.NETCore.Setup;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;

namespace LDSoft.AWS.ClientWrappers;

public class SecretManagerAPI
{
	public static string GetSecret(AWSOptions options, string secretName)
	{
		var request = new GetSecretValueRequest
					  {
						  SecretId = secretName,
						  VersionStage = "AWSCURRENT" // VersionStage defaults to AWSCURRENT if unspecified.
					  };

		var client = options.CreateServiceClient<IAmazonSecretsManager>();
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