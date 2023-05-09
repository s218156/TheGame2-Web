using MySqlConnector;
using TheGame2_Library.Models;

namespace TheGame2_Web.Services.DBComponents
{
    public class DBDeserializers
    {
        public UserModel DeserializeUser(MySqlDataReader dataReader)
        {
            UserModel model = new UserModel();
            model.id = dataReader.GetInt32("id");
            model.username = dataReader.GetString("username");
            model.fullname = dataReader.GetString("fullname");
            model.textureID = dataReader.GetInt32("textureID");
            model.password = "";
            model.refreshToken = dataReader.GetString("refreshToken");
            model.expiredDate = dataReader.GetDateTime("expiredDate");
            model.authToken = dataReader.GetString("authToken");
            return model;
        }

        public GameStatsModel DeserializeGameStats(MySqlDataReader dataReader)
        {
            GameStatsModel model = new GameStatsModel();
            model.id = dataReader.GetInt32("id");
            model.playerID = dataReader.GetInt32("playerID");
            model.onlineGameTime = dataReader.GetInt32("onlineGameTime");
            model.offlineGameTime = dataReader.GetInt32("offlineGameTime");
            return model;
        }

        public InGameUserModel DeserializeInGameUser(MySqlDataReader dataReader)
        {
            InGameUserModel model = new InGameUserModel();
            model.id = dataReader.GetInt32("id");
            model.userId = dataReader.GetInt32("userId");
            if(!dataReader.IsDBNull(dataReader.GetOrdinal("gameToken")))
                model.gameToken = dataReader.GetString("gameToken");
            return model;
        }

        public MultiplayerUserModel DeserializeMultiplayerUserModel(MySqlDataReader dataReader)
        {
            MultiplayerUserModel model = new MultiplayerUserModel();
            model.id = dataReader.GetInt32("id");
            model.textureId = dataReader.GetInt32("textureID");
            if (!dataReader.IsDBNull(dataReader.GetOrdinal("gameToken")))
                model.gameToken = dataReader.GetString("gameToken");
            model.username = dataReader.GetString("username");
            model.fullname = dataReader.GetString("fullname");
            return model;
        }


    }
}
