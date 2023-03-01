namespace TheGame2_Backend.Models
{
    public class SessionModel
    {
        public int Id { get; set; }
        public List<PlayerModel> players { get; set; }

        public SessionModel()
        {

        }
        public SessionModel(int id)
        {
            this.Id = id;
            this.players = new List<PlayerModel>();
        }
    }
}
