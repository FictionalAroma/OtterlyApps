namespace Otterly.Database.ActivityData.Bingo.DataObjects;

public class PlayerCard : MongoDataEntry
{
	public Guid TwitchUserID { get; set; }
}