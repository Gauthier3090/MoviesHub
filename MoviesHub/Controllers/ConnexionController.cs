using Isopoh.Cryptography.Argon2;
using Microsoft.AspNetCore.Mvc;
using MoviesHub.Models;
using MoviesHub.Models.Mappers;
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
        return View();
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Register([FromForm] UserForm userForm)
    {
        if (!ModelState.IsValid || userForm.Password == null || userForm.Email == null || userForm.Firstname == null ||
            userForm.Lastname == null) return View(userForm);
        string passwordHash = Argon2.Hash(userForm.Password);
        User user = _userService.Insert(userForm.Email, userForm.Firstname, userForm.Lastname, passwordHash,
            userForm.Old).ToModel();
        Console.WriteLine($"User created with id: {user.IdUser}");
        return RedirectToAction("Index", "Connexion");
    }
}