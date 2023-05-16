using Microsoft.AspNetCore.Mvc;
using TheGame2_Backend.Models;
using TheGame2_Backend.Services;
using TheGame2_Library.Models;

namespace TheGame2_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MultiplayerController : ControllerBase
    {
        MultiplayerService gameService;
        public MultiplayerController(MultiplayerService gameService) 
        {
            this.gameService = gameService;
        }

        [HttpPost("RefreshSessionData")]
        public List<PlayerModel> Refresh([FromBody] PlayerModel model)
        {
            return (List<PlayerModel>)gameService.sessions[0].players.Where(p => p.id != model.id).ToList();
        }

        [HttpPost("JoinSession")]
        public void JoinSession(PlayerModel player)
        {
            PlayerModel tmpPlayer = gameService.sessions[0].players.Where(p => p.id == player.id).FirstOrDefault();
            if (tmpPlayer == null)
            {
                gameService.sessions[0].players.Add(player);
                gameService.sessions[0].playersActivity.Add(new PlayerActivityModel() { player = player, lastActivityTime = DateTime.Now });
            }
            
        }

        [HttpPost("UpdatePlayerData")]
        public void UpdatePlayerData([FromBody] PlayerModel player)
        {
            PlayerModel tmpPlayer = gameService.sessions[0].players.Where(p => p.id == player.id).FirstOrDefault();
            if(tmpPlayer != null)
            {
                gameService.sessions[0].players.Where(p => p.id == player.id).FirstOrDefault().rectangle = player.rectangle;
                gameService.sessions[0].playersActivity.Where(p=>p.player.id==player.id).FirstOrDefault().lastActivityTime = DateTime.Now;
            }
            foreach(PlayerActivityModel playerActivity in gameService.sessions[0].playersActivity)
            {
                if(playerActivity.lastActivityTime > DateTime.Now.AddMinutes(5))
                {
                    gameService.sessions[0].RemovePlayer(playerActivity.player);
                }
            }
        }


    }
}
