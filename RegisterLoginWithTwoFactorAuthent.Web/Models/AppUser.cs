﻿using Microsoft.AspNetCore.Identity;

namespace RegisterLoginWithTwoFactorAuthent.Web.Models
{
    public class AppUser: IdentityUser
    {
        public sbyte? TwoFactor { get; set; }
    }
}
