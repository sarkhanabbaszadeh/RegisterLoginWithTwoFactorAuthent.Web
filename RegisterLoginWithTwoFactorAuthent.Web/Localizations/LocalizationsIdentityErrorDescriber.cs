using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace RegisterLoginWithTwoFactorAuthent.Web.Localizations
{
    public class LocalizationsIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError DuplicateUserName(string userName)
        {
            return new () { Code = "usernameDublicate", Description = $"'{userName}' adı daha əvvəl başqa istifadəçi tərəfindən alınmışdır." };

            //return base.DuplicateUserName(userName);
        }

        public override IdentityError DuplicateEmail(string email)
        {
            return new() { Code = "emailDublicate", Description = $"Bu '{email}' daha əvvəl qeydiyyata alınmışdır." };
            //return base.DuplicateEmail(email);
        }

        public override IdentityError PasswordTooShort(int length)
        {
            return new() { Code = "pass", Description = $"Şifrə ən azı '{length}' simvol uzunluğunda olmalıdır ." };
            //return base.PasswordTooShort(length);
        }
    }
}
