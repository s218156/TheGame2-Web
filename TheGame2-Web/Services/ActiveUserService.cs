using TheGame2_Backend;
using TheGame2_Library.Models;
using TheGame2_Web.Models;

namespace TheGame2_Web.Services
{
    public class ActiveUserService
    {
        DBConnector db;
        bool removalNeeded = false;

        private List<UserActivityModel> users = new List<UserActivityModel>();
        public ActiveUserService(DBConnector db)
        {
            this.db = db;
        }
        public void UpdateUserTime(UserModel user)
        {
            UserActivityModel dump = new UserActivityModel();
            if (!users.Where(m => m.user.username.Equals(user.username)).Any())
                users.Add(new UserActivityModel() { user = user, lastActivityTime = DateTime.UtcNow });

            foreach (UserActivityModel entity in users)
            {
                if (entity.user.username.Equals(user.username))
                    entity.lastActivityTime = DateTime.UtcNow;
                if (entity.lastActivityTime < DateTime.UtcNow.AddMinutes(-2))
                {
                    db.LogoutUser(entity.user);
                    removalNeeded = true;
                    dump = entity;
                }
            }
            if (removalNeeded)
            {
                users.Remove(dump);
                removalNeeded = false;
            }




        }

        public void RemoveUser(UserModel model)
        {
            foreach (UserActivityModel entity in users)
            {
                if (entity.user.username.Equals(model.username))
                    users.Remove(entity);
            }
        }

    }
}
