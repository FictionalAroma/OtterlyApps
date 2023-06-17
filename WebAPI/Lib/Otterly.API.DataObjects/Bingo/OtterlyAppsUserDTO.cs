using System;

namespace Otterly.API.DataObjects.Bingo
{
    public class OtterlyAppsUserDTO
    {
        public Guid UserID { get; set; }
        public int Test { get; set; }

        public bool Deleted { get; set; }
        public Guid TwitchID { get; set; }
    }
}
