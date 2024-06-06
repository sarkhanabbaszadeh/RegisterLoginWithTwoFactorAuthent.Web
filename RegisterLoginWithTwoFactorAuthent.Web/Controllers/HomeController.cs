using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RegisterLoginWithTwoFactorAuthent.Web.Models;
using RegisterLoginWithTwoFactorAuthent.Web.ViewModels;
using RegisterLoginWithTwoFactorAuthent.Web.Extensions;
using System.Diagnostics;
using RegisterLoginWithTwoFactorAuthent.Web.Enums;

namespace RegisterLoginWithTwoFactorAuthent.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult SignIn(string ReturnUrl="/")
        {
            TempData["ReturnUrl"] = ReturnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel request)
        {



			var hasUser=await _userManager.FindByEmailAsync(request.Email);

            if (hasUser == null)
            {
                ModelState.AddModelError(string.Empty, "E-poçta vəya şifrə yalnışdır.");
                return View();
            }

            bool userCheck = await _userManager.CheckPasswordAsync(hasUser, request.Password);

            if (userCheck)
            {
                await _userManager.ResetAccessFailedCountAsync(hasUser);
                await _signInManager.SignOutAsync();

                var result = await _signInManager.PasswordSignInAsync(hasUser, request.Password, request.RememberMe, true);

                if (result.RequiresTwoFactor)
                {
                    return RedirectToAction("TwoFactorSignIn","Home");
                }
                else
                {
                    return Redirect(TempData["ReturnUrl"].ToString());
                }

				if (result.Succeeded)
				{
					return Redirect(TempData["ReturnUrl"].ToString());
				}

				if (result.IsLockedOut)
				{
					ModelState.AddModelErrorList(new List<string>() { "Girisiniz 3 deqiqelik bloklanib." });
					return View();
				}

				ModelState.AddModelErrorList(new List<string>() { "E-poçta veya sifre yalnisdir.", $"Ugursuz giris sayi : {await _userManager.GetAccessFailedCountAsync(hasUser)}" });

				return View();
			}

			// var signInResult = await _signInManager.PasswordSignInAsync(hasUser, request.Password, request.RememberMe, true);

			return View();
		}

        public async Task<IActionResult> TwoFactorSignIn(string ReturnUrl = "/")
        {
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            TempData["ReturnUrl"] = ReturnUrl;

            switch((TwoFactor)user.TwoFactor)
            {
                case TwoFactor.MicrosoftGoogle:
                    break;
            }

            return View(new TwoFactorSignInViewModel() { TwoFactorType = (TwoFactor)user.TwoFactor, isRecoverCode = false, isRememberMe = false });
        }

        [HttpPost]
        public async Task<IActionResult> TwoFactorSignIn(TwoFactorSignInViewModel twofactor_model)
        {
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();

            ModelState.Clear();
            bool isSuccessAuth = false;

            if((TwoFactor)user.TwoFactor == TwoFactor.MicrosoftGoogle)
            {
                Microsoft.AspNetCore.Identity.SignInResult result;

                if (twofactor_model.isRecoverCode)
                {
                    result = await _signInManager.TwoFactorRecoveryCodeSignInAsync(twofactor_model.VerificationCode);
                }
                else
                {
                    result = await _signInManager.TwoFactorAuthenticatorSignInAsync(twofactor_model.VerificationCode, twofactor_model.isRememberMe, false);
                }

                if (result.Succeeded)
                {
                    isSuccessAuth = true;
                }
                else
                {
                    ModelState.AddModelError("", "Təsdiqləmə kodu səhvdir !");
                }

            }

            if (isSuccessAuth)
            {
                return Redirect(TempData["ReturnUrl"].ToString());
            }

            twofactor_model.TwoFactorType = (TwoFactor)user.TwoFactor;

            return View(twofactor_model);
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async  Task<IActionResult> SignUp(SignUpViewModel request)
        {

            if ((!ModelState.IsValid))
            {
                return View();
            }

            var identityResult = await _userManager.CreateAsync(new() { UserName = request.UserName, PhoneNumber = request.PhoneNumber, Email = request.Email, TwoFactor = 0 }, request.PasswordConfirm);


            if (identityResult.Succeeded)
            {
                TempData["SuccessMessage"] = "Qeydiyyat ugurludur !";

                return View();
            }

            ModelState.AddModelErrorList(identityResult.Errors.Select(x=>x.Description).ToList());

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
