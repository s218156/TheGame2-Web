using Microsoft.AspNetCore.Mvc;
using TheGame2_Backend;
using TheGame2_Library.Misc;
using TheGame2_Library.Models;
using TheGame2_Web.Services;

namespace TheGame2_Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameStatsController : ControllerBase
    {

        DBConnector db;
        ActiveUserService userService;

        public GameStatsController(DBConnector db, ActiveUserService userService)
        {
            this.db = db;
            this.userService = userService;
        }
        [HttpGet("GetGameStats")]
        public GameStatsModel GetGameStats()
        {
            string token = Request.Headers["auth"].ToString();
            try
            {
                UserModel user = CustomEncryption.DecryptUser(token);
                db.VerifyUser(user);
                userService.UpdateUserTime(user);
                return db.GetPlayerGameStats(user);
            }
            catch (Exception e)
            {
                return new GameStatsModel();
            }
        }
    }
}
