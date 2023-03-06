using MySqlConnector;
using TheGame2_Web.Models;

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
            return model;
        }
    }
}
