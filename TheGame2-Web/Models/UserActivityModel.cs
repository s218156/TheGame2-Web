using TheGame2_Library.Models;

namespace TheGame2_Web.Models
{
    public class UserActivityModel
    {
        public UserModel user { get; set; }
        public DateTime lastActivityTime { get; set; }
    }
}
