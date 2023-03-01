using TheGame2_Backend.Models;

namespace TheGame2_Backend.Services
{
    public class MultiplayerService
    {
        int sessionCount;
        public List<SessionModel> sessions { get; set; }
        public MultiplayerService()
        {
            this.sessionCount = 1;
            this.sessions = new List<SessionModel>();
            this.sessions.Add(new SessionModel(sessionCount));
        }
    }
    
}
