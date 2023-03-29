using Microsoft.AspNetCore.Mvc;
using TheGame2_Frontend.Services;
using TheGame2_Library.Exceptions;
using TheGame2_Library.Models;

namespace TheGame2_Frontend.Controllers
{
    public class UserHomeController : Controller
    {
        ApiCommunicationService apiService;

        public UserHomeController(ApiCommunicationService apiService)
        {
            this.apiService = apiService;
        }
        public async Task<IActionResult> Index()
        {
            string token = Request.Cookies["authToken"];
            try
            {
                Task<UserModel> user = apiService.GetUserData(token);
                return View(await user);
            }
            catch (TheGameWebException e)
            {
                return RedirectToAction("LoginE", "Login");
            }

        }
    }
}
