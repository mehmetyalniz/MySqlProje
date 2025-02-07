using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using MySqlProje.Models;
using MySqlProje.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;


namespace MySqlProje.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        [Authorize(Roles = "admin")]
        [HttpGet("admin")]
        public IActionResult AdminPanel()
        {
            var username = User.Identity.Name;
            return Ok($"Hoşgeldin {username}, Yönetici paneline hoşgeldiniz!");
        }

        [Authorize(Roles = "user")]
        [HttpGet("user")]
        public IActionResult UserPanel()
        {
            var username = User.Identity.Name;
            return Ok($"Hoşgeldin {username}, Kullanıcı paneline hoşgeldiniz!");
        }
    }
}

