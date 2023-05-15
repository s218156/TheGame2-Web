using Microsoft.AspNetCore.Mvc;

namespace TheGame2_Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class APIController : ControllerBase
    {
        [HttpGet("CheckApiAvalability")]
        public bool CheckApiAvalability() 
        {
            return true;
        }
    }
}
