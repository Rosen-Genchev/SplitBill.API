using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SplitBill.API.Config;
using SplitBill.API.Data;
using SplitBill.API.Dtos;
using SplitBill.API.Entities;
using SplitBill.API.Services;
using System.Linq;

namespace SplitBill.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly JwtTokenGenerator _jwtTokenGenerator;

        public AuthController(AppDbContext context, JwtTokenGenerator jwtTokenGenerator)
        {
            _context = context;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginDto loginDto)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == loginDto.Email);

            if (user == null || user.PasswordHash != loginDto.Password)
            {
                return Unauthorized("Invalid email or password");
            }

            var token = _jwtTokenGenerator.GenerateToken(user);
            return Ok(new { token });
        }
    }
}
