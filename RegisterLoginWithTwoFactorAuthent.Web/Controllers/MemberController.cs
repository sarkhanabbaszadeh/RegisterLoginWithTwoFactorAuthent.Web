using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RegisterLoginWithTwoFactorAuthent.Web.Enums;
using RegisterLoginWithTwoFactorAuthent.Web.Models;
using RegisterLoginWithTwoFactorAuthent.Web.Service;
using RegisterLoginWithTwoFactorAuthent.Web.ViewModels;

namespace RegisterLoginWithTwoFactorAuthent.Web.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly TwoFactorService _twoFactorService;

		public MemberController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, TwoFactorService twoFactorService)
		{
			_signInManager = signInManager;
			_userManager = userManager;
			_twoFactorService = twoFactorService;
		}

		public IActionResult Index()
        {
            return View();
        }

        public async Task Logout()
        {

            await _signInManager.SignOutAsync();

        }

		public async Task<IActionResult> TwoFactorWithAuthenticator()
		{
			var CurrentUser = _userManager.FindByNameAsync(User.Identity.Name).Result;

			string unformattedKey = await _userManager.GetAuthenticatorKeyAsync(CurrentUser);

			if (string.IsNullOrEmpty(unformattedKey))
			{
				await _userManager.ResetAuthenticatorKeyAsync(CurrentUser);

				unformattedKey = await _userManager.GetAuthenticatorKeyAsync(CurrentUser);
			}

			AuthenticatorViewModel authenticatorViewModel = new AuthenticatorViewModel();

			authenticatorViewModel.SharedKey = unformattedKey;

            authenticatorViewModel.AuthenticatorURL = _twoFactorService.GenerateQrCodeUrl(CurrentUser.Email, unformattedKey);

			return View(authenticatorViewModel);

		}

        [HttpPost]
        public async Task<IActionResult> TwoFactorWithAuthenticator(AuthenticatorViewModel authenticatorViewModel)
        {
            return View();
        }


        public IActionResult TwoFactorAuth()
        {
            var CurrentUser = _userManager.FindByNameAsync(User.Identity.Name).Result;
            return View(new AuthenticatorViewModel() { TwoFactorType = (TwoFactor)CurrentUser.TwoFactor});
        }

        [HttpPost]
        public async Task<IActionResult> TwoFactorAuth(AuthenticatorViewModel model)
        {
			var CurrentUser = _userManager.FindByNameAsync(User.Identity.Name).Result;

			switch (model.TwoFactorType)
            {
                case TwoFactor.None:
                    CurrentUser.TwoFactorEnabled = false;
                    CurrentUser.TwoFactor = (sbyte)TwoFactor.None;
                    TempData["message"] = "İki faktorlu doğrulama tipiniz heçbiri olaraq təyin olunub.";
                    break;

                case TwoFactor.MicrosoftGoogle:
                    return RedirectToAction("TwoFactorWithAuthenticator");

                default:
                    break;
            }

            await _userManager.UpdateAsync(CurrentUser);

            return View(model);
        }

	}
}
