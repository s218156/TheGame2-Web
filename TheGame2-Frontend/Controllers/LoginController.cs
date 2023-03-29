using Microsoft.AspNetCore.Mvc;
using TheGame2_Frontend.Models;
using TheGame2_Frontend.Services;
using TheGame2_Library.Exceptions;
using TheGame2_Library.Misc;
using TheGame2_Library.Models;

namespace TheGame2_Frontend.Controllers
{
    public class LoginController : Controller
    {
        private ApiCommunicationService apiService;

        public LoginController(ApiCommunicationService apiService)
        {
            this.apiService = apiService;
        }
        public IActionResult Index()
        {
            return RedirectToAction(nameof(Login));
        }

        public IActionResult Login()
        {
            return View();
        }

        public ActionResult LoginE()
        {
            ViewData["ErrorMessage"] = "You need to be logged in!";
            return View("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(UserModel model)
        {
            model.textureID = 0;
            try
            {
                model.password = CustomEncryption.EncryptPassword(model.password);
                UserModel user = await apiService.LoginUser(model);
                var cookieOption = new CookieOptions();
                cookieOption.Expires = DateTime.Now.AddDays(30);
                cookieOption.Path = "/";
                string cookieValue = CustomEncryption.GenerateToken(user.username, user.authToken, user.refreshToken);
                Response.Cookies.Append("authToken", cookieValue, cookieOption);
                return RedirectToAction("index", "UserHome");
            }
            catch (TheGameWebException e)
            {
                ViewData["ErrorMessage"] = e.Message;
                return View("Login");
            }
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SignUp(SignUpModel model)
        {

            if (model.password.Equals(model.passwordConfirm))
            {
                UserModel user = new UserModel();
                user.textureID = 0;
                user.expiredDate = DateTime.Now;
                user.password = model.password;
                user.username = model.username;
                user.fullname = model.fullname;
                user.authToken = "";
                user.refreshToken = "";

                try
                {
                    user.password = CustomEncryption.EncryptPassword(user.password);
                    apiService.CreateUser(user);
                    ViewData["message"] = "User has been created!";
                    return View("Login");
                }
                catch (TheGameWebException e)
                {
                    ViewData["ErrorMessage"] = e.Message;
                    return View("Login");
                }
            }
            else
            {
                ViewData["ErrorMessage"] = "Passwords are not the same!";
                return View("SignUp");
            }


        }

        public async Task<ActionResult> Logout()
        {
            string token = Request.Cookies["authToken"];
            try
            {
                await apiService.LogoutUser(token);
                ViewData["message"] = "Logout successful!";
                return View("Login");
            }
            catch (TheGameWebException e)
            {
                return RedirectToAction("LoginE", "Login");
            }
        }


    }
}
