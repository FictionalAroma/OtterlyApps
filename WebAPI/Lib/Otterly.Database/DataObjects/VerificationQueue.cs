using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Otterly.Database.UserData.DataObjects;

[Table("verificationqueue")]

public class VerificationQueueItem
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int VerificationID { get; set; }
	public string SessionID { get; set; }
	public int ItemIndex { get; set; }
	public DateTime ActivatedDateTime { get; set; }
	public bool? Result { get; set; }
	public DateTime VerifiedDateTime { get; set; }
	public DateTime ExpiryDateTime { get; set; }

	public Guid UserID { get; set; }
	public List<VerificationQueuePlayerLog> PlayerLogs { get; set; }
}