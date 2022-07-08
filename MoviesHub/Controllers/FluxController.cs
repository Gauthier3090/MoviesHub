using IMDbApiLib;
using IMDbApiLib.Models;
using Microsoft.AspNetCore.Mvc;
using MoviesHub.Models.Mappers;
using MoviesHub.Services;
using MoviesHub_BLL.DTO;
using MoviesHub_BLL.Services;

namespace MoviesHub.Controllers;

public class FluxController : Controller
{
    private const string ApiKey = "k_zf6s6fta";
    private readonly CacheService _cache = new();
    private readonly PublicationService _publicationService;

    public FluxController(PublicationService publicationService)
    {
        _publicationService = publicationService;
    }

    public IActionResult Index()
    {
        string? id = HttpContext.Session.GetString("Id");
        int creator = 0;
        if (id != null)
            creator = int.Parse(id);
        PublicationDto? publication = _publicationService.GetPublicationByUser(creator);
        Console.WriteLine(publication);
        return View(publication);
    }

    public IActionResult Create()
    {
        return View();
    }

    public async Task<ActionResult> MoviesList(string? title)
    {
        List<SearchResult> search = new();
        SearchData? response;
        ApiLib apiLib = new(ApiKey);
        string? key = title?.ToLower() ?? null;
        if (key == null) return RedirectToAction("Create", "Flux");
        if (_cache.ContainsItem(key))
            response = (SearchData?)_cache.GetItem(key);
        else
            response = await apiLib.SearchMovieAsync(key);
        response?.Results?.ForEach(m => search.Add(m));
        if (title != null && response != null)
            _cache.LastSearchMovie(title, response);
        return View(search);
    }

    public async Task<ActionResult> Publish(string id)
    {
        TitleData? movie;
        ApiLib apiLib = new(ApiKey);
        if (_cache.ContainsItem(id))
            movie = (TitleData?)_cache.GetItem(id);
        else
            movie = await apiLib.TitleAsync(id, Language.fr);
        return View(movie);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult PublishButton(string title, string plot, string image)
    {
        string? id = HttpContext.Session.GetString("Id");
        int creator = 0;
        if (id != null)
            creator = int.Parse(id);
        if (ModelState.IsValid)
            _publicationService.Insert(title, plot, image, creator)?.ToModel();
        return RedirectToAction("Index", "Flux");
    }
}