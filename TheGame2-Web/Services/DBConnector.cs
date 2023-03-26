using TheGame2_Backend.Services.DBComponents;
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
                throw e;
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
                throw new Exception("User not authorized");
        }
    }
}