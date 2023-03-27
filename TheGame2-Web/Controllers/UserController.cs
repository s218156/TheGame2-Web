using Microsoft.AspNetCore.Mvc;
using TheGame2_Backend;
using TheGame2_Library.Exceptions;
using TheGame2_Library.Misc;
using TheGame2_Library.Models;
using TheGame2_Web.Services;

namespace TheGame2_Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        DBConnector db;
        ActiveUserService userService;
        public UserController(DBConnector db, ActiveUserService userService)
        {
            this.db = db;
            this.userService = userService;
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
                userService.UpdateUserTime(user);
                return db.GetUserData(user);
            }
            catch (TheGameWebException e)
            {
                Response.StatusCode = Int32.Parse(e.ExceptionCode);
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
                userService.UpdateUserTime(user);
                db.VerifyUser(user);
            }
            catch (TheGameWebException e)
            {
                Response.StatusCode = Int32.Parse(e.ExceptionCode);
            }
        }

        [HttpPost("Create")]
        public void Create([FromBody] UserModel model)
        {
            try
            {
                model.password = CustomEncryption.DecryptPassword(model.password);
                model.password = CustomEncryption.EncryptPasswordForDatabase(model);
                db.AddUser(model);
            }
            catch (TheGameWebException e)
            {
                Response.StatusCode = Int32.Parse(e.ExceptionCode);
            }
        }
        [HttpGet("Logout")]
        public void LogoutUser()
        {
            string token = Request.Headers["auth"].ToString();
            try
            {
                UserModel user = CustomEncryption.DecryptUser(token);
                db.LogoutUser(user);
            }
            catch (TheGameWebException e)
            {
                Response.StatusCode = Int32.Parse(e.ExceptionCode);
            }


        }

    }
}
