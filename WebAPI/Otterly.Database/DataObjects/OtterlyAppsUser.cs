

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Otterly.Database.DataObjects
{
    public class OtterlyAppsUser : IdentityUser
	{
		[NotMapped]
		private Guid? _userIDLazy;

		[NotMapped] public Guid UserIDParsed => _userIDLazy ??= Guid.Parse(Id);
	}
}
