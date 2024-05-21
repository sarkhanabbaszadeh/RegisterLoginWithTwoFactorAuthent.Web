using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using RegisterLoginWithTwoFactorAuthent.Web.CustomValidations;
using RegisterLoginWithTwoFactorAuthent.Web.Localizations;
using RegisterLoginWithTwoFactorAuthent.Web.Models;

namespace RegisterLoginWithTwoFactorAuthent.Web.Extensions
{
    public static class StartupExtensions
    {

        public static void AddIdentityWithExt(this IServiceCollection services )
        {
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnoprstuwxyz0123456789_.";

                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;

            }).AddPasswordValidator<PasswordValidator>().AddUserValidator<UserValidator>
            ().AddErrorDescriber<LocalizationsIdentityErrorDescriber>().AddEntityFrameworkStores<AppDBContext>();
        }
    }
}
