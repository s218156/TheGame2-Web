namespace TheGame2_Library.Models
{
    public class UserModel
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string? fullname { get; set; }
        public int textureID { get; set; }
        public string? authToken { get; set; }
        public string? refreshToken { get; set; }
        public DateTime expiredDate { get; set; }
    }
}
