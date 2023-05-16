namespace TheGame2_Backend.Models
{
    public class SessionModel
    {
        public int Id { get; set; }

        public List<PlayerActivityModel> playersActivity { get; set; }
        public List<PlayerModel> players { get; set; }

        public SessionModel()
        {

        }
        public SessionModel(int id)
        {
            this.Id = id;
            this.players = new List<PlayerModel>();
        }
        public void RemovePlayer(PlayerModel player)
        {
            players.Remove(player);
            playersActivity.Remove(playersActivity.Where(p => p.player == player).FirstOrDefault());
        }
    }
}
