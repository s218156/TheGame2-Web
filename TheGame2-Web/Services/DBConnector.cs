using TheGame2_Backend.Services.DBComponents;
using TheGame2_Library.Exceptions;
using TheGame2_Library.Models;

namespace TheGame2_Backend
{
    public class DBConnector : DBConnectorBase
    {

        private string server;
        private string database;
        private string uid;
        private string password;
        public DBConnector(IConfiguration config) : base()
        {
            server = config.GetValue<string>("DBConfig:address");
            database = config.GetValue<string>("DBConfig:dbname");
            uid = config.GetValue<string>("DBConfig:username");
            password = config.GetValue<string>("DBConfig:password");
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";Connect Timeout=1000";

            Initialize(connectionString);
        }

        public List<UserModel> GetAllUsers()
        {
            string query = "SELECT * FROM TheGame.users;";
            return ProcessSelectAllUsers(query);
        }
        public UserModel GetUserByID(int id)
        {
            string query = "SELECT * FROM TheGame.users WHERE id=" + id + ";";
            return ProcessSelectUser(query);
        }

        private string GenerateToken()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuwxyz";
            return new string(Enumerable.Repeat(chars, 45).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private string GenerateToken(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuwxyz";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public UserModel LoginUser(UserModel model)
        {
            string query = "SELECT * FROM TheGame.users WHERE username like '" + model.username + "' and password like '" + model.password + "';";
            UserModel user = ProcessSelectUser(query);
            try
            {
                if (user.username.Equals(model.username))
                {
                    user.authToken = GenerateToken();
                    user.refreshToken = GenerateToken();
                    user.expiredDate = DateTime.Now.AddMinutes(30);
                    query = "UPDATE TheGame.users SET authToken='" + user.authToken + "' , LogedIn=1, refreshToken='" + user.refreshToken + "', expiredDate='" + user.expiredDate.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE (id=" + user.id + ");";
                    ProcessInsertUpdateQuery(query);
                    return user;
                }
                else
                    return new UserModel();
            }
            catch (Exception e)
            {
                return new UserModel();
            }
        }

        public UserModel GetUserData(UserModel model)
        {
            string query = "SELECT * FROM TheGame.users where username like '" + model.username + "' and authToken like '" + model.authToken + "'";
            return ProcessSelectUser(query);

        }

        public void VerifyUser(UserModel user)
        {
            string query = "SELECT * FROM TheGame.users where username like \"" + user.username + "\";";
            List<UserModel> list = ProcessSelectUsers(query);
            bool isAuthorised = false;
            foreach (UserModel listItem in list)
            {
                if (listItem.authToken == user.authToken)
                    isAuthorised = true;
            }
            if (!isAuthorised)
                throw new TheGameWebException("600", "User not authorized");
        }

        public void AddUser(UserModel model)
        {
            string query = "SELECT * FROM TheGame.users WHERE username like '" + model.username + "';";
            List<UserModel> users = ProcessSelectUsers(query);
            if (users.Count() == 0)
            {
                query = "INSERT INTO TheGame.users (username, password, fullname, authToken, refreshToken) VALUES ('" + model.username + "', '" + model.password + "','" + model.fullname + "', '" + GenerateToken() + "','" + GenerateToken() + "');";
                ProcessInsertUpdateQuery(query);
            }
            else
            {
                throw new TheGameWebException("600", "User already exists!");
            }
        }

        public void LogoutUser(UserModel model)
        {
            string query = "UPDATE TheGame.users SET logedIn=0, authToken='" + GenerateToken() + "', refreshToken='" + GenerateToken() + "' WHERE (username='" + model.username + "');";
            ProcessInsertUpdateQuery(query);
        }

        public GameStatsModel GetPlayerGameStats(UserModel model)
        {
            string query = "SELECT g.id as id, g.playerID as playerID, g.offlineGameTime as offlineGameTime, g.onlineGameTime as onlineGameTime FROM TheGame.GameStats as g INNER JOIN TheGame.users AS u ON u.id=g.playerID WHERE u.username like '" + model.username + "';";
            return ProcessSelectGameStats(query);
        }

        public string GetInGameUserGameToken(string login, string password)
        {
            string query = "select i.id as id, i.userId as userId, i.gameToken as gameToken from TheGame.users as u inner join TheGame.InGameUser as i on i.userId = u.id where u.username like '"+login+"' and u.password like '"+password+"';";
            InGameUserModel model = ProcessSelectInGameUser(query);
            try
            {
                if ((model.gameToken == null) || (model.gameToken.Length == 0)) {
                    model.gameToken = GenerateToken(150);
                    query = "UPDATE TheGame.InGameUser SET gameToken='" + model.gameToken + "' WHERE (id=" + model.id + ");";
                    ProcessInsertUpdateQuery(query);
                }
                return model.gameToken;
            }catch(Exception e)
            {
                throw new TheGameWebException("600", "Error while user auth");
            }
        }

        public void VerifyInGameUser(string username, string gameToken)
        {
            string query = "SELECT * FROM TheGame.users WHERE username like '" + username + "';";
            List<UserModel> users = ProcessSelectUsers(query);
            int id = users.FirstOrDefault().id;
            query = "SELECT * FROM TheGame.InGameUser WHERE userId = " + id + " and gameToken like " + gameToken + ";";
            InGameUserModel model = ProcessSelectInGameUser(query);
            if (model == null)
                throw new TheGameWebException("Can not verify");
        }
    }
}