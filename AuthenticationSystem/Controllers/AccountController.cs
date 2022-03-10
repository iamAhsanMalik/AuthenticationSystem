using Microsoft.AspNetCore.Mvc;

namespace AuthenticationSystem.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login()
        {
            return View();
        }
    }
}
