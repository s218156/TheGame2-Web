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
            }
            
        }

        [HttpPost("UpdatePlayerData")]
        public void UpdatePlayerData([FromBody] PlayerModel player)
        {
            PlayerModel tmpPlayer = gameService.sessions[0].players.Where(p => p.id == player.id).FirstOrDefault();
            if(tmpPlayer != null)
            {
                gameService.sessions[0].players.Where(p => p.id == player.id).FirstOrDefault().rectangle = player.rectangle;
            }
        }


    }
}
