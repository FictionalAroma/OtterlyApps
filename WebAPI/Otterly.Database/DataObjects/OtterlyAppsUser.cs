﻿

using Microsoft.AspNetCore.Identity;

namespace Otterly.Database.DataObjects
{
    public class OtterlyAppsUser : IdentityUser
    {
        public int Test { get; set; }
    }
}
