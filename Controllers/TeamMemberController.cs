using Microsoft.AspNetCore.Mvc;
using StudioIncantare.Dtos;
using StudioIncantare.Models;
using StudioIncantare.Repositories;
using Dapper;
using Microsoft.AspNetCore.Authorization;



namespace StudioIncantare.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamMemberController : ControllerBase
    {
        private readonly ITeamMemberRepository _repository;


        public TeamMemberController(ITeamMemberRepository repository)
        {
            this._repository = repository;
        }

        [HttpGet]

        public async Task<ActionResult> GetTeam()
        {
            var members = await _repository.GetAllAsync();

            return Ok(members.Select(m => new TeamMemberResponseDto
            {
                Id = m.id.ToString(),
                Name = m.Name,
                Role = m.Role,
                Bio = m.Bio,
                Image_Url = m.Image_url,
                Created_at = m.Created_at

            }));
        }

        [HttpPost]
        [Authorize]

        public async Task<ActionResult> InsertTeam(TeamMemberCreateDto dto)
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
            return Ok(new { member.id });
        }

        [HttpPut("{id}")]
        [Authorize]

        public async Task<ActionResult> UpdateMemberTeam(string id, [FromBody] TeamMemberCreateDto dto)
        {
            var member = await _repository.GetByIdAsync(id);

            if (member == null) return NotFound(new { mensagem = "Membro nao encontrado " });

            member.Name = dto.Name;
            member.Role = dto.Role;
            member.Bio = dto.Bio;
            member.Image_url = dto.Image_Url;

            await _repository.UpdateAsync(member);
            return Ok(new { mensagem = "Membro atualizado com sucesso " });

        }

        [HttpDelete("{id}")]
        [Authorize]

        public async Task<ActionResult> DeleteMemberTeam(string id)
        {
            var member = await _repository.GetByIdAsync(id);

            if (member == null) return NotFound(new { mensagem = "Membro nao encontrado " });

            await _repository.DeleteAsync(id);
            return Ok(new { mensagem = "Membro deletado com sucesso " });
        }
    }
}
