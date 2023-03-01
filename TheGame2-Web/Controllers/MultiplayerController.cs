using Microsoft.AspNetCore.Mvc;
using TheGame2_Backend.Models;
using TheGame2_Backend.Services;

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

        [HttpGet("Refresh")]
        public List<PlayerModel> Refresh()
        {
            return gameService.sessions[0].players;
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
