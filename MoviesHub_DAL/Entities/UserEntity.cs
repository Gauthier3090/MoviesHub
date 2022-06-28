namespace MoviesHub_DAL.Entities;

public class UserEntity
{
    public int IdUser { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public int Old { get; set; }
}