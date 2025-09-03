using StudioIncantare.Dtos;
using StudioIncantare.Models;
using StudioIncantare.Repositories;

namespace StudioIncantare.Services
{
    public interface ITeamMemberService
    {
        Task<IEnumerable<TeamMemberResponseDto>> GetAllAsync();
        Task<TeamMember?> GetByIdAsync(string id);
        Task<string> InsertAsync(TeamMemberCreateDto dto);
        Task<TeamMember?> UpdateAsync(string id, TeamMemberCreateDto dto);
        Task<bool> DeleteAsync(string id);
    }

    public class TeamMemberService : ITeamMemberService
    {
        private readonly ITeamMemberRepository _repository;

        public TeamMemberService(ITeamMemberRepository repository)
        {
            this._repository = repository;
        }

        public async Task<string> InsertAsync(TeamMemberCreateDto dto)
        {

            var member = new TeamMember
            {
                id = Guid.NewGuid().ToString(),
                Name = dto.Name,
                Role = dto.Role,
                Bio = dto.Bio,
                Image_url = dto.Image_Url,
                Created_at = DateTime.UtcNow
            };
            await _repository.InsertAsync(member);
            return member.id;
        }

        public async Task<TeamMember?> GetByIdAsync(string id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<TeamMemberResponseDto>> GetAllAsync()
        {
            var members = await _repository.GetAllAsync();
            return members.Select(c => new TeamMemberResponseDto
            {
                Id = c.id,
                Name = c.Name,
                Role = c.Role,
                Bio = c.Bio,
                Image_Url = c.Image_url,
                Created_at = c.Created_at

            });
        }

        public async Task<TeamMember?> UpdateAsync(string id, TeamMemberCreateDto dto)
        {
            var member = await _repository.GetByIdAsync(id);

            if (member == null) return null;

            member.Name = dto.Name;
            member.Role = dto.Role;
            member.Bio = dto.Bio;
            member.Image_url = dto.Image_Url;
            member.Created_at = DateTime.UtcNow;

            await _repository.UpdateAsync(member);
            return member;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var member = await _repository.GetByIdAsync(id);

            if (member == null) return false;

            await _repository.DeleteAsync(id);

            return true;
        }
    }
}
