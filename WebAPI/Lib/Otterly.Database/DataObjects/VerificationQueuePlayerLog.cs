using System.ComponentModel.DataAnnotations.Schema;

namespace Otterly.Database.UserData.DataObjects;

[Table("verificationqueue-playerlog")]

public class VerificationQueuePlayerLog
{
	public string PlayerID { get; set; }
	public int ItemIndex { get; set; }

	public int VerificationID { get; set; }
}