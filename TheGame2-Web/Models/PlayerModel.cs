using System.Numerics;
using TheGame2_Backend.Models.Nowy_folder;

namespace TheGame2_Backend.Models
{
    public class PlayerModel
    {
        public int id { get; set; }
        public string fullname { get; set; }
        public PlayerRectangle rectangle { get; set; }
        public int textureID { get; set; }
        public bool crouch { get; set; }
        public bool isAlive { get; set; }

        public bool direction { get; set; }
        public bool isOnMove { get; set; }
        public bool isJumping { get; set; }
        public bool isFalling { get; set; }

        

    }
}
