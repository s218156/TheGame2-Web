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
    }
}
