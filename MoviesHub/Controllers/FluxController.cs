using IMDbApiLib;
using IMDbApiLib.Models;
using Microsoft.AspNetCore.Mvc;
using MoviesHub.Models;
using MoviesHub.Services;

namespace MoviesHub.Controllers;

public class FluxController : Controller
{
    private const string ApiKey = "k_zf6s6fta";
    private readonly CacheService _cache = new();

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Create()
    {
        return View();
    }

    public async Task<ActionResult> MoviesList([FromForm] PublicationForm publicationForm)
    {
        List<SearchResult> search = new();
        SearchData? response;
        ApiLib apiLib = new(ApiKey);
        string? key = publicationForm.Title?.ToLower() ?? null;
        if (key == null) return RedirectToAction("Create", "Flux");
        if (_cache.ContainsItem(key))
            response = (SearchData?)_cache.GetItem(key);
        else
            response = await apiLib.SearchMovieAsync(key);
        response?.Results?.ForEach(m => search.Add(m));
        if (publicationForm.Title != null && response != null)
            _cache.LastSearchMovie(publicationForm.Title, response);
        return View(search);
    }

    public async Task<ActionResult> Publish(string id)
    {
        TitleData? movie;
        ApiLib apiLib = new(ApiKey);
        if (_cache.ContainsItem(id))
            movie = (TitleData?)_cache.GetItem(id);
        else
            movie = await apiLib.TitleAsync(id);
        return View(movie);
    }
}