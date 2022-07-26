using MoviesWorld_BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using MoviesWorld_BLL.DTO;


namespace MoviesWorld.Controllers;

public class FollowController : Controller
{
    private readonly UserService _userService;
    private readonly FollowService _followService;

    public FollowController(UserService userService, FollowService followService)
    {
        _userService = userService;
        _followService = followService;
    }

    public IActionResult Index()
    {
        string? user = HttpContext.Session.GetString("Id");
        if (user == null) return View();
        int target = int.Parse(user);
        IEnumerable<FollowDto> followers = _followService.GetFollows(target);
        return View(followers);
    }


    public JsonResult SearchUser(string? search)
    {
        if (search == null)
            return Json(null);
        IEnumerable<UserDto?> user = _userService.Search(search);
        return Json(new { message = user });
    }

    [Route("Follow/AddUser/{follow}")]
    public IActionResult AddUser(int follow)
    {
        string? user = HttpContext.Session.GetString("Id");
        if (user == null) return RedirectToAction("Index", "Follow");
        int target = int.Parse(user);
        Console.WriteLine(_followService.FollowerExist(target, follow));
        if (follow != target && _followService.FollowerExist(target, follow) == 0)
            _followService.Insert(target, follow);
        return RedirectToAction("Index", "Follow");
    }

    [Route("Follow/DeleteUser/{follow}")]
    public IActionResult DeleteUser(int follow)
    {
        string? user = HttpContext.Session.GetString("Id");
        if (user == null) return RedirectToAction("Index", "Follow");
        int target = int.Parse(user);
        TempData["Success"] = "L'abonnement n'a pas pu être supprimé !";
        if (_followService.DeleteUser(target, follow))
            TempData["Success"] = "Abonnement supprimé avec succès !";
        Console.WriteLine(TempData["Success"]);
        return RedirectToAction("Index", "Follow");
    }
}