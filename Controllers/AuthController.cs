using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notes_API.Data;
using Notes_API.DTOs;
using Notes_API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
namespace Notes_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController:ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;
        public AuthController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDto dto) 
        {
            var user = new User
            {
                UserName = dto.UserName,
                Password = dto.Password,
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(user);
        }
        [HttpPost("login")]
        public async Task<ActionResult> login(LoginDto dto) 
        {
            var user = await _context.Users.FirstOrDefaultAsync
            (
                x => x.UserName == dto.Username
            );

            if (user == null)
                return Unauthorized();
            if (user.Password != dto.Password)
            {
                return Unauthorized();
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName)
            };
            var key = new SymmetricSecurityKey
                (Encoding.UTF8.GetBytes
                (
                    _configuration["Jwt:Key"])
                );
            var creds = new SigningCredentials
                (key,
                SecurityAlgorithms.HmacSha256
                );
            var token = new JwtSecurityToken
                (claims:claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(jwt);
        }
    }
}
