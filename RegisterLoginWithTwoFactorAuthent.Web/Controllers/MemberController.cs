using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RegisterLoginWithTwoFactorAuthent.Web.Models;

namespace RegisterLoginWithTwoFactorAuthent.Web.Controllers
{
    public class MemberController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;

        public MemberController(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task Logout()
        {

            await _signInManager.SignOutAsync();

        }
    }
}
