using IMDbApiLib;
using IMDbApiLib.Models;
using Microsoft.AspNetCore.Mvc;
using MoviesWorld.Models.Mappers;
using MoviesWorld.Services;
using MoviesWorld_BLL.DTO;
using MoviesWorld_BLL.Services;

namespace MoviesWorld.Controllers;

public class FluxController : Controller
{
    private const string ApiKey = "k_zf6s6fta";
    private readonly CacheService _cache = new();
    private readonly PublicationService _publicationService;
    private readonly FollowService _followService;


    public FluxController(PublicationService publicationService, FollowService followService)
    {
        _publicationService = publicationService;
        _followService = followService;

    }

    public IActionResult Index()
    {
        string? id = HttpContext.Session.GetString("Id");
        int creator = 0;
        if (id != null)
            creator = int.Parse(id);
        IEnumerable<PublicationDto> publications = _publicationService.GetPublicationByUser(creator);
        IEnumerable<FollowDto> followers = _followService.GetFollows(creator);
        foreach (FollowDto item in followers)
        {
            publications = publications.Concat(_publicationService.GetPublicationByUser(item.Follow!.Id));
        }

        publications = publications.OrderByDescending(x => x.CreatedAt).ToList();
        return View(publications);
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