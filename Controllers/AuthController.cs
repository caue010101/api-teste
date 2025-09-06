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

        private readonly AuthService _authService;

        public AuthController(AuthService service)
        {

            this._authService = service;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {


            var token = await _authService.AuthenticateLogin(dto);

            if (token == null) return Unauthorized(new { mensagem = "Usuario ou senha invalidos " });

            return Ok(new { token });


        }
    }
}
