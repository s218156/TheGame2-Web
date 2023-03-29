using MySqlConnector;
using TheGame2_Library.Models;
using TheGame2_Web.Services.DBComponents;

namespace TheGame2_Backend.Services.DBComponents
{
    public class DBConnectorBase
    {
        protected MySqlConnection connection;
        protected DBDeserializers deserializer;

        public DBConnectorBase()
        {

            deserializer = new DBDeserializers();
        }
        public void Initialize(string connectionString)
        {
            this.connection = new MySqlConnection(connectionString);
        }

        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {

                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        Console.WriteLine("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        protected void ProcessInsertUpdateQuery(string query)
        {
            if (this.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        protected List<UserModel> ProcessSelectDevices(string query)
        {
            List<UserModel> list = new List<UserModel>();
            if (this.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                    list.Add(deserializer.DeserializeUser(dataReader));

                dataReader.Close();
                this.CloseConnection();
            }
            return list;
        }

        protected int ProcessSelectInt(string query)
        {
            int nextID = -1;
            if (this.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                    nextID = dataReader.GetInt32("AUTO_INCREMENT");


                dataReader.Close();
                this.CloseConnection();
            }
            return nextID;
        }

        protected string ProcessSelectString(string query)
        {
            string value = "";
            if (this.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                    value = dataReader.GetString(0);


                dataReader.Close();
                this.CloseConnection();
            }
            return value;
        }

        protected List<UserModel> ProcessSelectAllUsers(string query)
        {
            List<UserModel> users = new List<UserModel>();
            if (this.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                    users.Add(deserializer.DeserializeUser(dataReader));


                dataReader.Close();
                this.CloseConnection();
            }
            return users;
        }
        protected UserModel ProcessSelectUser(string query)
        {
            List<UserModel> users = ProcessSelectUsers(query);
            if (users.Count() == 1)
                return users.FirstOrDefault();
            else
                return null;
        }

        protected List<UserModel> ProcessSelectUsers(string query)
        {
            List<UserModel> users = new List<UserModel>();
            if (this.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                    users.Add(deserializer.DeserializeUser(dataReader));


                dataReader.Close();
                this.CloseConnection();
            }
            return users;
        }

        protected GameStatsModel ProcessSelectGameStats(string query)
        {
            List<GameStatsModel> gameStats = new List<GameStatsModel>();
            if (this.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                    gameStats.Add(deserializer.DeserializeGameStats(dataReader));


                dataReader.Close();
                this.CloseConnection();
            }
            return gameStats.FirstOrDefault();
        }




    }
}