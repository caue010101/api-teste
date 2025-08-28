namespace StudioIncantare.Models
{



    public class Contact
    {
        public string Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime Created_At { get; set; } = DateTime.UtcNow;

    }
}
