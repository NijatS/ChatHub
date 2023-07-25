using ChatHub.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ChatHub.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> _signin;
        private readonly UserManager<AppUser> _userMAnager;

        public AccountController(SignInManager<AppUser> signin, UserManager<AppUser> userMAnager)
        {
            _signin = signin;
            _userMAnager = userMAnager;
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AppUser appUser)
        {

            AppUser? user = await _userMAnager.FindByNameAsync(appUser.UserName);

            var result = await _signin.PasswordSignInAsync(user, appUser.PasswordHash, true, true);

            if (!result.Succeeded)
            {

                ModelState.AddModelError("", "USername or password incorrect");
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signin.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}
