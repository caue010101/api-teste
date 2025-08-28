using Dapper;
using StudioIncantare.Models;
using StudioIncantare.Infrastructure;
using StudioIncantare.Dtos;

namespace StudioIncantare.Repositories
{

    public interface IContactRepository

    {
        Task AddAsync(Contact contact);
        Task<IEnumerable<ContactResponseDto>> GetAllAsync();

    }
    public class ContactRepository : IContactRepository
    {
        private readonly DapperContext _context;

        public ContactRepository(DapperContext context)
        {
            this._context = context;
        }

        public async Task AddAsync(Contact contact)
        {
            var sql = @"INSERT INTO contacts (Id, Name, Email, Message, Created_at )
              VALUES(@Id, @Name, @Email, @Message, @Created_at)";

            using var connection = _context.CreateDbConnection();
            await connection.ExecuteAsync(sql, contact);
        }

        public async Task<IEnumerable<ContactResponseDto>> GetAllAsync()
        {
            var sql = @"SELECT CAST(id AS CHAR(36)) AS Id, name AS Nome, email AS Email, created_at AS CreatedAt FROM contacts";
            using var connection = _context.CreateDbConnection();
            return await connection.QueryAsync<ContactResponseDto>(sql);
        }
    }
}
