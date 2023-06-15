using System.ComponentModel.DataAnnotations;

namespace Otterly.API.ClientLib.DTO
{
    public class OtterlyAppsUserDTO
    {
        public Guid UserID { get; set; }
        public int Test { get; set; }

        public bool Deleted { get; set; }
		public Guid TwitchID { get; set; }
	}
}
