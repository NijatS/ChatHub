using Microsoft.AspNetCore.Mvc;

namespace ChatHub.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
