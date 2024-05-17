using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RegisterLoginWithTwoFactorAuthent.Web.Areas.Admin.Models;
using RegisterLoginWithTwoFactorAuthent.Web.Models;

namespace RegisterLoginWithTwoFactorAuthent.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public HomeController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> UserList()
        {
            var userList = await _userManager.Users.ToListAsync();

            var userViewModelList = userList.Select(x => new UserViewModel()
            {
                ID = x.Id,
                Email = x.Email,
                Name = x.UserName
            }).ToList();

            return View(userViewModelList);
        }
    }
}
