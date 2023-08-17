using Newtonsoft.Json;

namespace LDSoft.AWS.Models;

public class RDSCredentials
{
		[JsonProperty("username")]
		public string Username { get; set; }

		[JsonProperty("password")]
		public string Password { get; set; }

		[JsonProperty("engine")]
		public string Engine { get; set; }

		[JsonProperty("host")]
		public string Host { get; set; }

		[JsonProperty("port")]
		public int Port { get; set; }

		[JsonProperty("dbInstanceIdentifier")]
		public string DbInstanceIdentifier { get; set; }

		[JsonProperty("databaseName")]
		public string DatabaseName { get; set; }
}