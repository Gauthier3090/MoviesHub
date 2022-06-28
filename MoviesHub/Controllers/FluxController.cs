using Isopoh.Cryptography.Argon2;
using Microsoft.AspNetCore.Mvc;
using MoviesHub.Models;
using MoviesHub.Models.Mappers;
using MoviesHub_BLL.Services;

namespace MoviesHub.Controllers;

public class FluxController : Controller
{
    private readonly UserService _userService;

    public FluxController(UserService userService)
    {
        _userService = userService;
    }

    public IActionResult Index()
    {
        return View();
    }
}