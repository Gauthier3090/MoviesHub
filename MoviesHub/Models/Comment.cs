using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MoviesWorld.Models;

public class Comment
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

public class CommentForm
{
    [Required(ErrorMessage = "Veuillez entrer un titre.")]
    [StringLength(300)]
    public string? Headline { get; set; }

    [Required(ErrorMessage = "Veuillez entrer une description.")]
    [StringLength(300)]
    public string? Body { get; set; }
}