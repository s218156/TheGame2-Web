using Microsoft.AspNetCore.Mvc;
using TheGame2_Backend;
using TheGame2_Backend.Models;

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
        public UserModel GetUserByID([FromBody]int id)
        {
            return db.GetUserByID(id);
        }
    }
}
