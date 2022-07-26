namespace MoviesWorld_BLL.DTO;

public class PublicationDto
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; }
    public UserDto? User { get; set; }
    public int Creator { get; set; }
}