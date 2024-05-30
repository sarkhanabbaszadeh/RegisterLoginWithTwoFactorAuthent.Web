using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RegisterLoginWithTwoFactorAuthent.Web.Enums;
using RegisterLoginWithTwoFactorAuthent.Web.Models;
using RegisterLoginWithTwoFactorAuthent.Web.ViewModels;

namespace RegisterLoginWithTwoFactorAuthent.Web.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

		public MemberController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
		{
			_signInManager = signInManager;
			_userManager = userManager;
		}

		public IActionResult Index()
        {
            return View();
        }

        public async Task Logout()
        {

            await _signInManager.SignOutAsync();

        }

        public IActionResult TwoFactorAuth()
        {
            var CurrentUser = _userManager.FindByNameAsync(User.Identity.Name).Result;
            return View(new AuthenticatorViewModel() { TwoFactorType = (TwoFactor)CurrentUser.TwoFactor});
        }
    }
}
