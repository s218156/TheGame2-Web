using Microsoft.AspNetCore.Mvc;

namespace TheGame2_Web.Services
{
    public class DBConnector : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
