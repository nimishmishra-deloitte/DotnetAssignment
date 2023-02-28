using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Dotnet_Assignment.Models;
using Dotnet_Assignment;

namespace CourseAPI.Controllers;

    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;  
        private ProjectContext _context;
        public LoginController(IConfiguration config,ProjectContext context)
        {
            _config= _config;
            _context=context;
        }
        
        
        
        [HttpPost, Route("login")]
        public IActionResult Login(LoginDto loginDTO)
        {
            User user=_context.Users.SingleOrDefault(user=>user.Username==loginDTO.Username);
            try
            {
               
                if (string.IsNullOrEmpty(loginDTO.Username) ||
                string.IsNullOrEmpty(loginDTO.Password))
                    return BadRequest("Username and/or Password not specified");
                if (loginDTO.Username.Equals(user.Username) &&
                loginDTO.Password.Equals(user.Password))
                {
                    var secretKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes("Thisismysecretkey"));
                    var signinCredentials = new SigningCredentials
                   (secretKey, SecurityAlgorithms.HmacSha256);
                    if (user.Roles == "Admin")
                    {
                        var jwtSecurityToken = new JwtSecurityToken(
                            "https://localhost:7261",  
                            "https://localhost:7261", 
                            claims: new List<Claim>()
                            {
                                new Claim(ClaimTypes.Role, "Admin"),
                                new Claim(ClaimTypes.Role, "ProjectManager")
                            },
                            expires: DateTime.Now.AddMinutes(10),
                            signingCredentials: signinCredentials
                        );
                         return Ok(new JwtSecurityTokenHandler().
                    WriteToken(jwtSecurityToken));
                    }
                    else if (user.Roles == "ProjectManager")
{
    var jwtSecurityToken = new JwtSecurityToken(
        "https://localhost:7261",  
        "https://localhost:7261", 
        claims: new List<Claim>()
        {           
            new Claim(ClaimTypes.Role, "ProjectManager")
        },
        expires: DateTime.Now.AddMinutes(10),
        signingCredentials: signinCredentials
    );
     return Ok(new JwtSecurityTokenHandler().
                    WriteToken(jwtSecurityToken));
}
                   
                }
            }
            catch
            {
                return BadRequest
                ("An error occurred in generating the token");
            }
            return Unauthorized();
        }

   

    }