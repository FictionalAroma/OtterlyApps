using Newtonsoft.Json;

namespace Otterly.API.ExternalAPI.Objects;

public class TwitchExtensionPubSubMessage
{
	[JsonProperty("message")]
	public string Message { get; set; }

	[JsonProperty("broadcaster_id")]
	public string BroadcasterId { get; set; }

	[JsonProperty("target")] 
	public string[] Target { get; set; } = { "broadcast" };

	[JsonProperty("is_global_broadcast")] 
	public bool GlobalBroadcast { get; set; } = false;

}