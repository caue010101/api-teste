using StudioIncantare.Dtos;
using StudioIncantare.Repositories;
using StudioIncantare.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]

public class ContactController : ControllerBase
{
    private readonly IContactRepository _repository;

    public ContactController(IContactRepository repository)
    {
        this._repository = repository;
    }

    [HttpPost]
    public async Task<IActionResult> CreateContact([FromBody] CreateContactDto dto)
    {
        var contact = new Contact
        {
            Id = Guid.NewGuid().ToString(),
            Name = dto.Name,
            Email = dto.Email,
            Message = dto.Message,
            Created_At = DateTime.UtcNow
        };

        await _repository.AddAsync(contact);
        return StatusCode(201, new { id = contact.Id });

    }
    [Authorize]
    [HttpGet]

    public async Task<IActionResult> GetContacts()
    {
        var contact = await _repository.GetAllAsync();
        return Ok(contact);
    }
}
