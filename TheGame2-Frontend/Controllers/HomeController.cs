using Microsoft.AspNetCore.Mvc;

namespace TheGame2_Frontend.Controllers
{
    public class HomeController : Controller
    {


        public HomeController()
        {

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Galery()
        {
            return View();

        }


    }
}