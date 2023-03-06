using Microsoft.AspNetCore.Mvc;

namespace TheGame2_Web.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
