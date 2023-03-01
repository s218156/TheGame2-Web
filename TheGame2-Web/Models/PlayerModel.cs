using System.Numerics;
using TheGame2_Backend.Models.Nowy_folder;

namespace TheGame2_Backend.Models
{
    public class PlayerModel
    {
        public int id { get; set; }
        public PlayerRectangle rectangle { get; set; }
        public int textureID { get; set; }

    }
}
