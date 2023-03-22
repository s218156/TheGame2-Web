using Microsoft.AspNetCore.Mvc;

namespace TheGame2_Frontend.Controllers
{
    public class UserHomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
