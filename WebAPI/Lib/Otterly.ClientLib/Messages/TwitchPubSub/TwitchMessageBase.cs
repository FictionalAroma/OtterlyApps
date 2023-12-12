using LDSoft.APIClient;

namespace Otterly.API.ClientLib.Messages.TwitchPubSub;

public class TwitchMessageBase 
{
	protected TwitchMessageBase(TwitchMessageType messageType) { MessageType = messageType; }
	public TwitchMessageType MessageType { get; set; }
}

public enum TwitchMessageType
{
	None = 0,
	BingoItemVerified,
	BingoTicketStamped,
}