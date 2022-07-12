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
        IEnumerable<UserDto?> user = _userService.GetAll();
        if (search == null)
            return Json(null);
        user = user.Where(x => x!.Firstname!.StartsWith(search));
        return Json(new { message = user });
    }
}