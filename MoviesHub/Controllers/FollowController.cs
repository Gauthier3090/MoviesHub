using MoviesWorld_BLL.Services;
using Microsoft.AspNetCore.Mvc;
using MoviesWorld_BLL.DTO;

namespace MoviesWorld.Controllers;

public class FollowController : Controller
{
    private readonly UserService _userService;

    public FollowController(UserService userService)
    {
        _userService = userService;
    }

    public IActionResult Index()
    {
        return View();
    }


    public JsonResult SearchUser(string? search)
    {
        if (search == null)
            return Json(null);
        IEnumerable<UserDto?> user = _userService.Search(search);
        return Json(new { message = user });
    }
}