using Microsoft.AspNetCore.Mvc;
using MoviesWorld.Models;
using MoviesWorld_BLL.DTO;
using MoviesWorld_BLL.Services;

namespace MoviesWorld.Controllers;

public class CommentController : Controller
{
    private readonly CommentService _commentService;
    private readonly PublicationService _publicationService;

    public CommentController(CommentService commentService, PublicationService publicationService)
    {
        _commentService = commentService;
        _publicationService = publicationService;
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
        {
            return View(comment);
        }

        bool success = int.TryParse(HttpContext.Session.GetString("Id"), out int userId);
        if (success)
        {
            _commentService.Insert(comment.Headline, comment.Body, userId, id);
        }

        return RedirectToAction("Index", "Flux");
    }

    [Route("Publication/{id}/Comment/Details")]
    public IActionResult Details(int id)
    {
        if (_publicationService.PublicationExist(id) == null)
        {
            return RedirectToAction("NotFound404", "Error");
        }

        IEnumerable<CommentDto> comments = _commentService.GetCommentsByPublication(id);

        return View(comments);
    }
}