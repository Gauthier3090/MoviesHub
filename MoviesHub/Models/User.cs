using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MoviesHub.Models;

public class User
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

public class UserForm
{
    [Required(ErrorMessage = "Entrez votre email.")]
    [StringLength(300)]
    [DisplayName("Email")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Entrez votre prénom.")]
    [StringLength(150)]
    [DisplayName("Prénom")]
    public string? Firstname { get; set; }

    [Required(ErrorMessage = "Entrez votre mot de passe.")]
    [StringLength(200)]
    [DataType(DataType.Password)]
    [DisplayName("Mot de passe")]
    [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$")]
    public string? Password { get; set; }

    [Required(ErrorMessage = "Entrez votre mot de passe.")]
    [StringLength(200)]
    [DataType(DataType.Password)]
    [Compare(nameof(Password))]
    [DisplayName("Confirmez votre mot de passe")]
    public string? PasswordConfirm { get; set; }

    [Required(ErrorMessage = "Entrez votre nom.")]
    [StringLength(200)]
    [DisplayName("Nom")]
    public string? Lastname { get; set; }

    [Required(ErrorMessage = "Entrez votre date de naissance.")]
    [DisplayName("Date de naissance")]
    public DateTime Birthdate { get; set; }

    [Required(ErrorMessage = "Choissisez une photo de profil.")]
    [DisplayName("Photo de profil")] public IFormFile? Image { get; set; }
}

public class UserConnexionForm
{
    [DisplayName("Email")]
    [Required(ErrorMessage = "L'email est obligatoire")]
    [EmailAddress(ErrorMessage = "Le format de l'adresse de courriel est éroné :o")]
    [DataType(DataType.EmailAddress)]
    public string? Email { get; set; }

    [DisplayName("Mot de Passe")]
    [Required(ErrorMessage = "Le mot de passe est obligatoire")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
}

public class UserUpdateForm
{
    [Required(ErrorMessage = "Entrez votre email.")]
    [StringLength(300)]
    [DisplayName("Email")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Entrez votre mot de passe.")]
    [StringLength(200)]
    [DataType(DataType.Password)]
    [DisplayName("Mot de passe")]
    [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$")]
    public string? Password { get; set; }

    [Required(ErrorMessage = "Entrez votre mot de passe.")]
    [StringLength(200)]
    [DataType(DataType.Password)]
    [Compare(nameof(Password))]
    [DisplayName("Confirmez votre mot de passe")]
    public string? PasswordConfirm { get; set; }

    [Required(ErrorMessage = "Choissisez une photo de profil.")]
    [DisplayName("Photo de profil")] public IFormFile? Image { get; set; }
}