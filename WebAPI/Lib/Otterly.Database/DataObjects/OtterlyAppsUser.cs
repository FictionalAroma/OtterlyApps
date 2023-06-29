using System.ComponentModel.DataAnnotations;

namespace Otterly.Database.UserData.DataObjects
{
	public class OtterlyAppsUser
	{
		[Key]
		public Guid UserID { get; set; } = Guid.NewGuid();

		public string TwitchID { get; set; }

		public string Username { get; set; }

		public string ProfileImagePath { get; set; }

		public List<UserAuth> AuthList { get; set;}
	}
}
