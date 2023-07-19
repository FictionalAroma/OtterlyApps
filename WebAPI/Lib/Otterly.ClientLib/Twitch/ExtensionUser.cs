using System.ComponentModel;

namespace Otterly.API.ClientLib.Twitch;

public struct ExtensionUser
{
	
	public string BroadcasterID { get; init; }
	public string UserID { get; init; }

	public Role UserRole { get; init; }

}

public enum Role
{
	[Description("viewer")]
	Viewer = 1,
	[Description("moderator")]
	Mod = 2,
	[Description("broadcaster")]
	Streamer = 3
}