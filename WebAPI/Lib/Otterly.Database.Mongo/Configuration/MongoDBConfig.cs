namespace Otterly.Database.ActivityData.Configuration;

public class MongoDBConfig
{
	public string ConnectionString { get; set; } = null!;
	public string DatabaseName { get; set; } = null!;
	public string Username { get; set; } = string.Empty;
	public string Password { get; set; } = string.Empty;
}