
using TheGame2_Backend.Services.DBComponents;

namespace TheGame2_Backend
{
    public class DBConnector : DBConnectorBase
    {

        private string server;
        private string database;
        private string uid;
        private string password;
        public DBConnector() : base()
        {
            server = "10.8.8.19";
            database = "WebCon";
            uid = "webconapi";
            password = "webconapi";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";Connect Timeout=1000";

            Initialize(connectionString);
        }

    }

}