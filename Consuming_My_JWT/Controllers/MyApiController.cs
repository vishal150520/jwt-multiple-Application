
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Consuming_My_JWT.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MyApiController : ControllerBase
    {
        
        [HttpGet]
        [Route("UserList")]
        public IActionResult Get()
        {
            var users = new List<string>
            {
                "Satinder Singh",
                "Amit Sarna",
                "Davin Jon"
            };
            return Ok(users);

          

            
        }
    }
}
