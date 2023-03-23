
using TheGame2_Backend.Services.DBComponents;
using TheGame2_Backend.Models;

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
            string query = "SELECT * FROM TheGame.users WHERE id="+id+";";
            return ProcessSelectUser(query);
        }

    }

}