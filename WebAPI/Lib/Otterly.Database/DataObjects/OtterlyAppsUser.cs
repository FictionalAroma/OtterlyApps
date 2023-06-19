using System.ComponentModel.DataAnnotations;

namespace Otterly.Database.UserData.DataObjects
{
	public class OtterlyAppsUser
	{
		[Key]
		public Guid UserID { get; set; } = Guid.NewGuid();
        public int Test { get; set; }

		public Guid TwitchID { get; set; }
    }
}
