using Microsoft.AspNetCore.Http;

namespace MoviesHub_DAL.Entities;

public class UserEntity
{
    public int Id { get; set; }
    public string? Email { get; set; }
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public string? Password { get; set; }
    public DateTime Birthdate { get; set; }
    public string? Image { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; }
}