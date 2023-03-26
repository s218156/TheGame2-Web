using Microsoft.AspNetCore.Mvc;
using TheGame2_Frontend.Services;
using TheGame2_Library.Models;

namespace TheGame2_Frontend.ViewComponents
{
    public class UserStatusViewComponent : ViewComponent
    {
        ApiCommunicationService apiService;
        public UserStatusViewComponent(ApiCommunicationService service)
        {
            this.apiService = service;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string token = Request.Cookies["authToken"];
            if (token.Split(",").Length == 3)
            {
                try
                {
                    UserModel user = await apiService.GetUserData(token);
                    return View("data", user);
                }
                catch (Exception e)
                {
                    return View();
                }
            }
            else
                return View();
        }
    }
}
