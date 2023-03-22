using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TheGame2_Frontend.Models;

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
    }
}