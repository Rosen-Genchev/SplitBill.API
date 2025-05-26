using Microsoft.AspNetCore.Mvc;
using SplitBill.API.Data;
using SplitBill.API.Dtos;
using SplitBill.API.Entities;
using SplitBill.API.Services;

namespace SplitBill.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly JwtTokenGenerator _jwtTokenGenerator;
        private readonly PasswordService _passwordService;

        public AuthController(AppDbContext context, JwtTokenGenerator jwtTokenGenerator)
        {
            _context = context;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserRegisterDto registerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            if (_context.Users.Any(u => u.Email == registerDto.Email))
            {
                return BadRequest("User with this email already exists.");
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

            var user = new User
            {
                Email = registerDto.Email,
                PasswordHash = passwordHash
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            var token = _jwtTokenGenerator.GenerateToken(user);
            return Ok(new { token });
        }


        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _context.Users.FirstOrDefault(u => u.Email == loginDto.Email);

            if (user == null || !_passwordService.VerifyPassword(user.Id.ToString(), user.PasswordHash, loginDto.Password))
            {
                return Unauthorized("Invalid email or password");
            }


            var token = _jwtTokenGenerator.GenerateToken(user);
            return Ok(new { token });
        }
    }
}
