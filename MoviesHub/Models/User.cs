using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MoviesHub.Models;

public class User
{
    public int IdUser { get; set; }
    [DisplayName("Email")] public string? Email { get; set; }
    [DisplayName("Prénom")] public string? Firstname { get; set; }
    [DisplayName("Nom")] public string? Lastname { get; set; }
    [DisplayName("Âge")] public int Old { get; set; }
}

public class UserForm
{
    [DisplayName("Email")]
    [Required(ErrorMessage = "L'email est obligatoire")]
    public string? Email { get; set; }

    [DisplayName("Mot de Passe")]
    [Required(ErrorMessage = "Le mot de passe est obligatoire")]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$")]
    public string? Password { get; set; }

    [DisplayName("Confirmation")]
    [Required(ErrorMessage = "Le mot de passe est obligatoire")]
    [DataType(DataType.Password)]
    [Compare(nameof(Password))]
    public string? PasswordConfirm { get; set; }

    [DisplayName("Prénom")]
    [Required(ErrorMessage = "Le prénom est obligatoire")]
    public string? Firstname { get; set; }

    [DisplayName("Nom")]
    [Required(ErrorMessage = "Le nom est obligatoire")]
    public string? Lastname { get; set; }

    [DisplayName("Âge")]
    [Required(ErrorMessage = "L'âge est obligatoire")]
    public int Old { get; set; }
}