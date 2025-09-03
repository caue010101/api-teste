using StudioIncantare.Models;
using StudioIncantare.Repositories;
using StudioIncantare.Dtos;

namespace StudioIncantare.Services
{
    public interface IContactService
    {
        Task<IEnumerable<ContactResponseDto>> GetAllAsync();
        Task AddAsync(CreateContactDto contact);
    }

    public class ContactService : IContactService
    {
        private readonly IContactRepository _repository;

        public ContactService(IContactRepository repository)
        {
            this._repository = repository;
        }

        public async Task<IEnumerable<ContactResponseDto>> GetAllAsync()
        {
            var contacts = await _repository.GetAllAsync();
            return contacts.Select(c => new ContactResponseDto
            {
                Id = c.Id,
                Nome = c.Nome,
                Email = c.Email,
                CreatedAt = c.CreatedAt
            });
        }

        public async Task AddAsync(CreateContactDto dto)
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
        }
    }


}
