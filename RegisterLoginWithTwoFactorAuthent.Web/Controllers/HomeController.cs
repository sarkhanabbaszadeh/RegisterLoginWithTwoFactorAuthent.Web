using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RegisterLoginWithTwoFactorAuthent.Web.Models;
using RegisterLoginWithTwoFactorAuthent.Web.ViewModels;
using System.Diagnostics;

namespace RegisterLoginWithTwoFactorAuthent.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userManager;

        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
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

            var identityResult = await _userManager.CreateAsync(new() { UserName = request.UserName, PhoneNumber = request.PhoneNumber, Email = request.Email }, request.PasswordConfirm);


            if (identityResult.Succeeded)
            {
                TempData["SuccessMessage"] = "Qeydiyyat ugurludur !";

                return View();
            }

            foreach (IdentityError item in identityResult.Errors)
            {
                ModelState.AddModelError(string.Empty, item.Description);
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
