using Microsoft.AspNetCore.Mvc;
using StudioIncantare.Dtos;
using StudioIncantare.Models;
using StudioIncantare.Repositories;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using StudioIncantare.Services;



namespace StudioIncantare.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamMemberController : ControllerBase
    {
        private readonly ITeamMemberService _service;


        public TeamMemberController(ITeamMemberService service)
        {
            this._service = service;
        }

        [HttpGet]

        public async Task<ActionResult> GetTeamAsync()

        {
            var members = await _service.GetAllAsync();

            if (members == null)
            {
                return NotFound(new { mensagem = "nenhum membro encontrado" });
            }

            return Ok(members);
        }

        [HttpGet("{id}")]

        public async Task<ActionResult> GetByIdAsync(string id)
        {
            var member = await _service.GetByIdAsync(id);

            if (member == null)
            {
                return NotFound(new { mensagem = "membro nao encontrado " });
            }
            return Ok(member);
        }

        [HttpPost]
        [Authorize]

        public async Task<ActionResult> InsertAsync(TeamMemberCreateDto dto)
        {

            var member = await _service.InsertAsync(dto);

            if (member == null) return NotFound(new { mensagem = "membro nao encontrado " });

            return Ok(new { mensagem = "membro cadastrado com sucesso " });


        }

        [HttpPut("{id}")]
        [Authorize]

        public async Task<ActionResult> UpdateMemberTeam(string id, [FromBody] TeamMemberCreateDto dto)
        {
            var update = await _service.UpdateAsync(id, dto);

            if (update == null) return NotFound(new { mensagem = "membro nao encontrado " });

            return Ok(new { mensagem = "membro atualizado com sucesso " });

        }

        [HttpDelete("{id}")]
        [Authorize]

        public async Task<ActionResult> DeleteMemberTeam(string id)
        {
            var delete = await _service.DeleteAsync(id);

            if (!delete) return NotFound(new { mensagem = "membro nao encontrado" });

            return Ok(new { mensagem = "membro deletado com sucesso " });
        }
    }
}
