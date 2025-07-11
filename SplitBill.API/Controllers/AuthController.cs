﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public AuthController(AppDbContext context, JwtTokenGenerator jwtTokenGenerator, PasswordService passwordService)
        {
            _context = context;
            _jwtTokenGenerator = jwtTokenGenerator;
            _passwordService = passwordService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserRegisterDto registerDto)
        {
            if (_context.Users.Any(u => u.Email == registerDto.Email))
            {
                return BadRequest("Email already exists");
            }

            var fullName = registerDto.FullName ?? "";
            var nameParts = fullName.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
            var firstName = nameParts.Length > 0 ? nameParts[0] : "";
            var lastName = nameParts.Length > 1 ? nameParts[1] : "";

            var user = new User
            {
               
                Username = registerDto.Username,
                Email = registerDto.Email,
                FullName = registerDto.FullName,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password)
            };


            _context.Users.Add(user);
            _context.SaveChanges();

            var token = _jwtTokenGenerator.GenerateToken(user);

            return Ok(new { token });
        }




        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto loginDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
            if (user == null || !_passwordService.VerifyPassword(loginDto.Password, user.PasswordHash))
                return Unauthorized("Invalid credentials");

            var token = _jwtTokenGenerator.GenerateToken(user);

            return Ok(new
            {
                token,
                user = new { user.Id, user.Email, user.FullName, user.Role }
            });
        }
    }

}

