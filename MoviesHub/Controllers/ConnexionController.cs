using Isopoh.Cryptography.Argon2;
using Microsoft.AspNetCore.Mvc;
using MoviesWorld.Models.Mappers;
using MoviesWorld_BLL.DTO;
using MoviesWorld_BLL.Services;
using MoviesWorld.Models;
using MoviesWorld.Services;

namespace MoviesWorld.Controllers;

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
                HttpContext.Session.SetString("Id", user.Id.ToString());
                HttpContext.Session.SetString("Email", user.Email ?? "not found");
                HttpContext.Session.SetString("Firstname", user.Firstname ?? "not found");
                HttpContext.Session.SetString("Lastname", user.Lastname ?? "not found");
                HttpContext.Session.SetString("Birthdate", user.Birthdate.ToShortDateString());
                HttpContext.Session.SetString("Image", user.Image ?? "not found");
                HttpContext.Session.SetInt32("IsConnected", 1);
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

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Register([FromForm] UserForm userForm)
    {
        if (!ModelState.IsValid || userForm.Password == null || userForm.Email == null || userForm.Firstname == null ||
            userForm.Lastname == null || userForm.Image == null)
        {
            TempData["error"] = "Veuillez remplir tous les champs";
            return View(userForm);
        }
        if (_userService.GetByEmail(userForm.Email) != null)
        {
            TempData["error"] = "L'email est déjà utilisé !";
            return View(userForm);
        }
        string passwordHash = Argon2.Hash(userForm.Password);
        ImageService imageUser = new(userForm.Image);
        string? filenameImage = imageUser.FileName;
        imageUser.SaveImage();
        if (filenameImage != null)
        {
            _userService.Insert(userForm.Email, userForm.Firstname, userForm.Lastname, passwordHash,
                userForm.Birthdate, filenameImage)?.ToModel();
            TempData["success"] = "Vous êtes désormais inscrit !";
        }
        return View();
    }

    public IActionResult UpdateUser()
    {
        string? idstring = HttpContext.Session.GetString("Id");
        if (idstring == null) return View();
        int id = int.Parse(idstring);
        UserDto? user = _userService.GetById(id);
        if (user == null) return View();
        HttpContext.Session.SetString("Image", user.Image ?? "not found");
        HttpContext.Session.SetInt32("IsConnected", 1);
        return View();
    }

    public IActionResult UpdateButtonUser([FromForm] UserUpdateImage userForm)
    {
        if (!ModelState.IsValid || userForm.Image == null)
            return RedirectToAction("UpdateUser", "Connexion");

        string? idstring = HttpContext.Session.GetString("Id");
        if (idstring == null) return RedirectToAction("UpdateUser", "Connexion");
        int id = int.Parse(idstring);

        ImageService imageUser = new(userForm.Image);
        string? filenameImage = imageUser.FileName;
        imageUser.DeleteImage(HttpContext.Session.GetString("Image") ?? "null");
        imageUser.SaveImage();

        _userService.UpdateImage(id, filenameImage);

        return RedirectToAction("UpdateUser", "Connexion");
    }
}