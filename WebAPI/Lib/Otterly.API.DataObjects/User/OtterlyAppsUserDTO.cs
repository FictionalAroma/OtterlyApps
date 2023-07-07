using System;

namespace Otterly.API.DataObjects.User
{
    public class OtterlyAppsUserDTO
    {
        public Guid UserID { get; set; }
		public string TwitchID { get; set; }

        public string UserName { get; set; }

        public string ProfileImagePath { get; set; }
		public string EmailAddress { get; set; }
	}
}
