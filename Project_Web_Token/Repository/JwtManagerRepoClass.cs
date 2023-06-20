using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Project_Web_Token.Database;
using Project_Web_Token.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static System.Net.WebRequestMethods;

namespace Project_Web_Token.Repository
{
    public class JwtManagerRepoClass : JwtMangerRepocs
    {
        private readonly StamensProjectContext _projectContext;
        private readonly IConfiguration _configuration;
        //private readonly RoleManager<IdentityRole> _roleManager;
        public JwtManagerRepoClass(StamensProjectContext context,IConfiguration configuration)
        {
            this._projectContext = context;
            this._configuration = configuration;
            

            
        }
        public TbEmployee RegisterForm([FromForm]TbEmployee employee)
        {
            _projectContext.TbEmployees.Add(employee);
            _projectContext.SaveChanges();
            return employee;
        }
        public tokens addAuthenticate([FromBody]Validations validations)
        {
            var employee=new TbEmployee();
            var tokenHandler = new JwtSecurityTokenHandler();
            var validateUsers=_projectContext.TbEmployees.FirstOrDefault(x=>x.EmailId == validations.EmailId && x.Passwords==validations.Passwords);
            if(validateUsers != null)
            {
                 
                var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
                var Issuer = _configuration["JWT:Issuer"];

                var auidence = _configuration["JWT:Auidence"];
                var signing=new SigningCredentials(Key,SecurityAlgorithms.HmacSha256);
                var expires = DateTime.UtcNow.AddMinutes(10);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    //Subject = new ClaimsIdentity(new Claim[]
                    //{v                       
                    //    new Claim(ClaimTypes.Name,validations.EmailId),
                    //    new Claim(ClaimTypes.Role,employee.Ename)

                    //}),


                    SigningCredentials = signing,
                    Issuer = Issuer,
                    Audience = auidence,
                    Expires = expires,  
                    
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return new tokens { Token=tokenHandler.WriteToken(token) };
            }
            else
            {
                return null;
            }
         
        }

      
    }
}
