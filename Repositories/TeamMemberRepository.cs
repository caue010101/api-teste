using StudioIncantare.Dtos;
using StudioIncantare.Infrastructure;
using Dapper;
using StudioIncantare.Models;

namespace StudioIncantare.Repositories
{
    public interface ITeamMemberRepository
    {
        Task<IEnumerable<TeamMember>> GetAllAsync();
        Task<TeamMember?> GetByIdAsync(string id);
        Task InsertAsync(TeamMember member);
        Task UpdateAsync(TeamMember member);
        Task DeleteAsync(string id);
    }
    public class TeamMemberRepository : ITeamMemberRepository
    {
        private readonly DapperContext _context;

        public TeamMemberRepository(DapperContext context)
        {
            this._context = context;
        }
        public async Task<IEnumerable<TeamMember>> GetAllAsync()
        {
            var sql = @"SELECT 
                  CAST(id AS CHAR(36)) AS id, 
                  name, 
                  role, 
                  bio, 
                  image_url, 
                  created_at AS Created_at 
                FROM team_members";
            using var connection = _context.CreateDbConnection();
            return await connection.QueryAsync<TeamMember>(sql);
        }

        public async Task<TeamMember?> GetByIdAsync(string id)
        {
            var sql = @"SELECT 
                  CAST(id AS CHAR(36)) AS id, 
                  name, 
                  role, 
                  bio, 
                  image_url, 
                  created_at AS Created_at 
                FROM team_members 
                WHERE id = @id";
            using var connection = _context.CreateDbConnection();
            return await connection.QueryFirstOrDefaultAsync<TeamMember?>(sql, new { id });

        }

        public async Task InsertAsync(TeamMember member)
        {
            var sql = @"INSERT INTO team_members (id, name, role, bio, image_url, created_at)
            VALUES(@Id, @Name, @Role, @Bio, @Image_Url, @Created_At)";

            var connection = _context.CreateDbConnection();
            await connection.ExecuteAsync(sql, member);

        }

        public async Task UpdateAsync(TeamMember member)
        {
            var sql = @"UPDATE team_members SET
                      name = @Name,
                      role = @Role,
                      bio = @Bio,
                      image_url = @Image_Url
                      WHERE id = @Id";
            using var connection = _context.CreateDbConnection();
            await connection.ExecuteAsync(sql, member);
        }

        public async Task DeleteAsync(string id)
        {
            var sql = @"DELETE FROM team_members WHERE id = @Id";
            using var connection = _context.CreateDbConnection();
            await connection.ExecuteAsync(sql, new { id });
        }
    }
}
