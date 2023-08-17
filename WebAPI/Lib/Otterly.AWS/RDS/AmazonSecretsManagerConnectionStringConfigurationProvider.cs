using System.Text.Json;
using Amazon.Extensions.NETCore.Setup;
using LDSoft.AWS.ClientWrappers;
using LDSoft.AWS.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

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

			var creds = JsonConvert.DeserializeObject<RDSCredentials>(secret);


			if (creds != null)
			{
				var result =
					$"Server={creds.Host};Database={creds.DbInstanceIdentifier};user={creds.Username};password={creds.Password};";
				Data.Add($"ConnectionStrings:{_secretName}", result);

			}

		}


	}
}