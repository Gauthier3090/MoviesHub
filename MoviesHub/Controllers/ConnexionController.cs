using Isopoh.Cryptography.Argon2;
using Microsoft.AspNetCore.Mvc;
using MoviesHub.Models;
using MoviesHub.Models.Mappers;
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

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Index([FromForm] UserConnexionForm userConnexionForm)
    {
        if (!ModelState.IsValid || userConnexionForm.Email == null || userConnexionForm.Password == null)
            return View(userConnexionForm);
        string passwordHash = _userService.GetPassword(userConnexionForm.Email);
        if (Argon2.Verify(passwordHash, userConnexionForm.Password))
            return RedirectToAction("Index", "Flux");
        TempData["ErrorConnexion"] = "L'email ou le mot de passe est incorrect. Veuillez réessayer !";
        return View(userConnexionForm);
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
        _userService.Insert(userForm.Email, userForm.Firstname, userForm.Lastname, passwordHash,
            userForm.Old).ToModel();
        return RedirectToAction("Index", "Connexion");
    }
}