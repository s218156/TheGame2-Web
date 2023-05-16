
namespace TheGame2_Backend.Models
{
    public class PlayerActivityModel
    {
        public PlayerModel player { get; set; }
        public DateTime lastActivityTime { get; set; }
    }
}
