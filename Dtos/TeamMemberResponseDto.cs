namespace StudioIncantare.Dtos
{
    public class TeamMemberResponseDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public string Image_Url { get; set; } = string.Empty;
        public DateTime Created_at { get; set; }
    }
}
