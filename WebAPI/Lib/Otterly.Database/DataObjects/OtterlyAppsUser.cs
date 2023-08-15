using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Otterly.Database.UserData.DataObjects
{
	[Table("otterlyappsusers")]
	public class OtterlyAppsUser
	{
		[Key]
		public Guid UserID { get; set; } = Guid.NewGuid();
		public string ExternalAuthID { get; set; }
		public string TwitchID { get; set; }

		public string Username { get; set; }
		public string EmailAddress { get; set; }

		public string ProfileImagePath { get; set; }

		public List<UserAuth> AuthList { get; set;}
	}
}
