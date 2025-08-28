using Dapper;
using StudioIncantare.Infrastructure;
using StudioIncantare.Models;

namespace StudioIncantare.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByUsernameAsync(string username);
    }
    public class UserRepository : IUserRepository
    {
        private readonly DapperContext _context;
        public UserRepository(DapperContext context)
        {
            this._context = context;
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            var sql = "SELECT CAST(id AS CHAR(36)) AS Id, username, password_hash FROM users WHERE username = @username";
            using var connection = _context.CreateDbConnection();
            return await connection.QuerySingleOrDefaultAsync<User>(sql, new { username });
        }


    }
}
