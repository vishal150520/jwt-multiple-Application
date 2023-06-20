using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Project_Web_Token.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersApiController : ControllerBase
    {
        [HttpGet]
        [Route("UserList")]
       
        public List<string> Get()
        {
            var users = new List<string>
        {
            "Satinder Singh",
            "Amit Sarna",
            "Davin Jon"
        };

            return users;
        }
    }
}
