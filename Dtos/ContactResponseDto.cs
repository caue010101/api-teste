namespace StudioIncantare.Dtos
{
    public class ContactResponseDto
    {
        public string Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
