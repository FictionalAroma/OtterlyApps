using System.Collections.Generic;
using Otterly.API.ClientLib.Objects.Bingo;

namespace Otterly.API.ClientLib.Messages.TwitchPubSub;

public class BingoTicketStampedMessage : TwitchMessageBase
{
	public BingoTicketStampedMessage() : base(TwitchMessageType.BingoTicketStamped) { }

	public VerificationQueueItemDTO UpdatedVerificationQueueItem { get; set; }

}