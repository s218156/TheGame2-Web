using Microsoft.AspNetCore.Mvc;
using TheGame2_Backend;
using TheGame2_Library.Misc;
using TheGame2_Library.Models;

namespace TheGame2_Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        DBConnector db;
        public UserController(DBConnector db)
        {
            this.db = db;
        }

        [HttpGet("GetAllUsers")]
        public List<UserModel> GetAllUsers()
        {
            return db.GetAllUsers();
        }

        [HttpPost("GetUserByID")]
        public UserModel GetUserByID([FromBody] int id)
        {
            return db.GetUserByID(id);
        }
        [HttpPost("Login")]
        public UserModel Login([FromBody] UserModel model)
        {
            model.password = CustomEncryption.DecryptPassword(model.password);
            Console.WriteLine(model.password);
            model.password = CustomEncryption.EncryptPasswordForDatabase(model);
            return db.LoginUser(model);
        }

        [HttpGet("GetUserData")]
        public UserModel GetUserData()
        {
            string token = Request.Headers["auth"].ToString();
            try
            {
                UserModel user = CustomEncryption.DecryptUser(token);
                db.VerifyUser(user);
                return db.GetUserData(user);
            }
            catch (Exception e)
            {
                Response.StatusCode = 600;
                Console.WriteLine("Different token");
                return new UserModel();
            }
        }

        [HttpGet("VerifyUser")]
        public void VerifyUser()
        {
            string token = Request.Headers["auth"].ToString();
            try
            {
                UserModel user = CustomEncryption.DecryptUser(token);
                db.VerifyUser(user);
            }
            catch (Exception e)
            {
                Response.StatusCode = 600;
            }

        }

    }
}
