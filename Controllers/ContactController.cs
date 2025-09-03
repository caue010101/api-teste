using StudioIncantare.Dtos;
using StudioIncantare.Repositories;
using StudioIncantare.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using StudioIncantare.Services;

[ApiController]
[Route("api/[controller]")]

public class ContactController : ControllerBase
{
    private readonly IContactService _service;

    public ContactController(IContactService service)
    {
        this._service = service;

    }

    [HttpPost]
    public async Task<IActionResult> CreateContact([FromBody] CreateContactDto dto)
    {
        var contact = _service.AddAsync(dto);
        return StatusCode(201, new { id = contact.Id });

    }
    [Authorize]
    [HttpGet]

    public async Task<IActionResult> GetContacts()
    {
        var contact = await _service.GetAllAsync();
        return Ok(contact);
    }
}
