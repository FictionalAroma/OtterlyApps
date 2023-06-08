using System.ComponentModel.DataAnnotations;

namespace Otterly.ClientLib.Bingo.DTO
{
	public class OtterlyAppsUserDTO
	{
		public Guid UserID { get; }
        public int Test { get; set; }

		public bool Deleted { get; set; }
    }
}
