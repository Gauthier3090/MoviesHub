using Isopoh.Cryptography.Argon2;
using Microsoft.AspNetCore.Mvc;
using MoviesHub.Models;
using MoviesHub.Models.Mappers;
using MoviesHub.Services;
using MoviesHub_BLL.DTO;
using MoviesHub_BLL.Services;

namespace MoviesHub.Controllers;

public class ConnexionController : Controller
{
    private readonly UserService _userService;

    public ConnexionController(UserService userService)
    {
        _userService = userService;
    }

    public IActionResult Index()
    {

        return View(new UserConnexionForm
        {
            Email = "gauthier.pladet@gmail.com",
            Password = "#Helloworld1"
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Index([FromForm] UserConnexionForm? userConnexionForm)
    {
        if (!ModelState.IsValid || userConnexionForm?.Email == null || userConnexionForm.Password == null)
            return View(userConnexionForm);
        string passwordHash = _userService.GetPassword(userConnexionForm.Email) ?? "null";
        if (Argon2.Verify(passwordHash, userConnexionForm.Password))
        {
            UserDto? user = _userService.GetByEmail(userConnexionForm.Email);
            if (user != null)
            {
                _userService.IsConnected(user.Id);
                HttpContext.Session.SetString("Email", user.Email ?? "not found");
                HttpContext.Session.SetString("Firstname", user.Firstname ?? "not found");
                HttpContext.Session.SetString("Lastname", user.Lastname ?? "not found");
                HttpContext.Session.SetString("Birthdate", user.Birthdate.ToShortDateString());
                HttpContext.Session.SetString("Image", user.Image ?? "not found");
                return RedirectToAction("Index", "Flux");
            }
        }
        TempData["ErrorConnexion"] = "L'email ou le mot de passe est incorrect. Veuillez réessayer !";
        return View(userConnexionForm);
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Register([FromForm] UserForm userForm)    {
        if (!ModelState.IsValid || userForm.Password == null || userForm.Email == null || userForm.Firstname == null ||
            userForm.Lastname == null || userForm.Image == null) return View(userForm);
        string passwordHash = Argon2.Hash(userForm.Password);
        ImageService imageUser = new(userForm.Image);
        string? filenameImage = imageUser.FileName;
        imageUser.SaveImage();
        if (filenameImage != null)
        {
            _userService.Insert(userForm.Email, userForm.Firstname, userForm.Lastname, passwordHash,
                userForm.Birthdate, filenameImage)?.ToModel();
        }
        return RedirectToAction("Index", "Connexion");
    }
}