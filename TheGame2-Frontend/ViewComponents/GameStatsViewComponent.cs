using Microsoft.AspNetCore.Mvc;
using TheGame2_Frontend.Services;
using TheGame2_Library.Models;

namespace TheGame2_Frontend.ViewComponents
{
    public class GameStatsViewComponent : ViewComponent
    {
        ApiCommunicationService apiService;
        public GameStatsViewComponent(ApiCommunicationService service)
        {
            this.apiService = service;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string token = Request.Cookies["authToken"];
            Task<GameStatsModel> gameStats = apiService.GetGameStatsForUser(token);
            return View(await gameStats);
        }
    }
}
