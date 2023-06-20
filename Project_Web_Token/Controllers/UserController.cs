using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_Web_Token.Repository;
using Project_Web_Token.Database;
using Microsoft.AspNetCore.Authorization;
using Project_Web_Token.Tokens;
using System.Net.Http.Headers;

namespace Project_Web_Token.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly JwtMangerRepocs _jwtMangerRepocs;
        public UserController(JwtMangerRepocs jwtMangerRepocs)
        {
            this._jwtMangerRepocs = jwtMangerRepocs;
            
        }
        [AllowAnonymous]
        [Route("Register")]
        [HttpGet]   
        public IActionResult Register([FromForm] TbEmployee employee)
        {
            _jwtMangerRepocs.RegisterForm(employee);
            return Ok(employee);

        }
        [AllowAnonymous]
        [HttpPost]
        [Route("Authorized")]
        public IActionResult Authorized([FromBody]Validations validations)
        {
            var token = _jwtMangerRepocs.addAuthenticate(validations);
            if(token == null)
            {
                return Unauthorized();
            }
            else
            {
                //Response.Headers.Add("Authorization", "Bearer " + token);
                return Ok(token);
                

            }
        }
    }
}
