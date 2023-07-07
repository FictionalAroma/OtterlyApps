using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Otterly.Database.UserData.DataObjects;

public class UserAuth
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int AuthID { get; set; }
	public enum AuthenticationType
	{
		Twitch = 1,
	}

	public AuthenticationType AuthType { get; set; }

	public string Token { get; set; }

	public string TokenRefresh { get; set; }

	public Guid UserID { get; set; }
}