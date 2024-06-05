﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RegisterLoginWithTwoFactorAuthent.Web.Models;
using RegisterLoginWithTwoFactorAuthent.Web.ViewModels;
using RegisterLoginWithTwoFactorAuthent.Web.Extensions;
using System.Diagnostics;

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
                    return RedirectToAction("TwoFactoLogin","Member");
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
