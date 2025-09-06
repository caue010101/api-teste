using StudioIncantare.Repositories;
using StudioIncantare.Models;
using StudioIncantare.Dtos;
using BCrypt.Net;

namespace StudioIncantare.Services
{



    public class AuthService
    {
        private readonly IUserRepository _repository;
        private readonly JwtService _jwtService;

        public AuthService(IUserRepository repository, JwtService jwt)
        {
            this._repository = repository;
            this._jwtService = jwt;
        }

        public async Task<string?> AuthenticateLogin(LoginDto dto)
        {
            var user = await _repository.GetByUsernameAsync(dto.Username);

            if (user == null) return null;

            bool valid = BCrypt.Net.BCrypt.Verify(dto.Password, user.password_hash);

            if (!valid) return null;

            return _jwtService.GenerateToken(user);
        }
    }
}
