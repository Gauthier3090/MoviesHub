namespace MoviesWorld_DAL.Entities;

public class CommentEntity
{
    public int Id { get; set; }
    public string? Headline { get; set; }
    public string? Body { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; }
    public int User { get; set; }
    public int Publication { get; set; }
}