using Microsoft.AspNetCore.Mvc;
using MoviesWorld.Models;
using MoviesWorld_BLL.DTO;
using MoviesWorld_BLL.Services;

namespace MoviesWorld.Controllers;

public class CommentController : Controller
{
    private readonly CommentService _commentService;

    public CommentController(CommentService commentService)
    {
        _commentService = commentService;
    }

    [Route("Publication/{id}/Comment/Create")]
    public IActionResult Create(int id)
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("Publication/{id}/Comment/Create")]
    public IActionResult Create(int id, [FromForm] CommentForm comment)
    {
        if (!ModelState.IsValid || comment.Headline == null || comment.Body == null)
            return View(comment);
        string? user = HttpContext.Session.GetString("Id");
        if (user == null) return View();
        int idUser = int.Parse(user);
        _commentService.Insert(comment.Headline, comment.Body, idUser, id);
        return RedirectToAction("Index", "Flux");
    }

    [Route("Publication/{id}/Comment/Details")]
    public IActionResult Details(int id)
    {
        IEnumerable<CommentDto> comments = _commentService.GetCommentsByPublication(id);
        return View(comments);
    }
}