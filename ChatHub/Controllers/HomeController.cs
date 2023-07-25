using ChatHub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ChatHub.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public HomeController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            List<AppUser> users = await _userManager.Users.ToListAsync();
            return View(users);
        }

        //public async Task<IActionResult> CreateUser()
        //{
        //    await _userManager.CreateAsync(new AppUser { Email = "Nicat@mail.ru", UserName = "NicatCode" }, "Nicat123@");
        //    await _userManager.CreateAsync(new AppUser { Email = "Isa@mail.ru", UserName = "JesusCode" }, "Jesus123@");
        //    await _userManager.CreateAsync(new AppUser { Email = "Sineray@mail.ru", UserName = "SinerayCode" }, "Sineray123@");
        //    await _userManager.CreateAsync(new AppUser { Email = "Dayday@mail.ru", UserName = "DaydayCode" }, "Dayday123@");
        //    return Json("Ok");
        //}
    }
}