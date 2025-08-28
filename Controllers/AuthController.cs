using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
using StudioIncantare.Dtos;
using StudioIncantare.Services;
using StudioIncantare.Repositories;

namespace StudioIncantare.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;
        private readonly IUserRepository _userRepository;

        public AuthController(JwtService jwtService, IUserRepository userRepository)
        {
            this._jwtService = jwtService;
            this._userRepository = userRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {


            var user = await _userRepository.GetByUsernameAsync(dto.Username);
            if (user == null) return Unauthorized("Usuario ou senha invalidos ");


            bool valid = BCrypt.Net.BCrypt.Verify(dto.Password, user.password_hash);

            if (!valid) return Unauthorized("Usuario ou senha invalidos ");

            var token = _jwtService.GenerateToken(user);

            return Ok(new { token });

        }
    }
}
