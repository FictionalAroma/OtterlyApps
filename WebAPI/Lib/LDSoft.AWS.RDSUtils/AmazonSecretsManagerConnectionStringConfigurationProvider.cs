using System.Text.Json;
using Amazon.Extensions.NETCore.Setup;
using LDSoft.AWS.ClientWrappers;
using LDSoft.AWS.Models;
using Microsoft.Extensions.Configuration;

namespace LDSoft.AWS.RDSUtils
{

	public class AmazonSecretsManagerConnectionStringConfigurationProvider : ConfigurationProvider
	{
		private readonly AWSOptions _awsOptions;
		private readonly string _region;
		private readonly string _secretName;

		public AmazonSecretsManagerConnectionStringConfigurationProvider(
			AWSOptions awsOptions,
			string region,
			string secretName)
		{
			_awsOptions = awsOptions;
			_region = region;
			_secretName = secretName;
		}

		public override void Load()
		{
			var secret = SecretManagerAPI.GetSecret(_awsOptions, _secretName);

			var creds = JsonSerializer.Deserialize<RDSCredentials>(secret);


			if (creds != null)
			{
				var result =
					$"Server={creds.Host};Database={creds.DbInstanceIdentifier};Uid={creds.Username};Pwd={creds.Password};";
				Data.Add($"ConnectionStrings:{_secretName}", result);

			}

		}


	}
}