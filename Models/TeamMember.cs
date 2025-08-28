namespace StudioIncantare.Models
{

    public class TeamMember
    {
        public string id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public string Image_url { get; set; } = string.Empty;
        public DateTime Created_at { get; set; }
    }
}
