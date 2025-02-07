using MySqlProje.Models;
using MySqlProje.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace JwtToken.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        // Sabit kullanıcı listesi
        private List<User> users = new List<User>
        {
            new User { Username = "admin", Password = "admin123", Role = "admin" },
            new User { Username = "user", Password = "user123", Role = "user" }
        };

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel login)
        {
            var user = users.SingleOrDefault(x => x.Username == login.Username && x.Password == login.Password);

            if (user == null)
            {
                return Unauthorized();
            }

            var token = JwtHelper.GenerateToken(user.Username, user.Role);

            return Ok(new
            {
                token,
                role = user.Role // Rol bilgisini de döndürelim
            });
        }

    }
}


