using IMDbApiLib;
using IMDbApiLib.Models;
using Microsoft.AspNetCore.Mvc;
using MoviesHub.Models;
using Newtonsoft.Json;

namespace MoviesHub.Controllers;

public class FluxController : Controller
{
    private const string ApiKey = "k_zf6s6fta";

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Create()
    {
        return View();
    }

    //public IActionResult MoviesList()
    //{
    //    List<SearchResult> response = (List<SearchResult>)TempData["Movies"];

    //    Console.WriteLine(response);
    //    return View(response);
    //}

    public async Task<ActionResult> MoviesList([FromForm]PublicationForm publicationForm)
    {
        List<SearchResult> search = new List<SearchResult>();
        try
        {
            ApiLib apiLib = new(ApiKey);
            SearchData? response = await apiLib.SearchMovieAsync(publicationForm.Title);
            response?.Results?.ForEach(m => search.Add(m));

            return View(search);
        }
        catch (HttpRequestException)
        {
            Console.WriteLine("An error occurred.");
        }
        catch (NotSupportedException)
        {
            Console.WriteLine("The content type is not supported.");
        }
        catch (JsonException)
        {
            Console.WriteLine("Invalid JSON.");
        }
        return RedirectToAction("Create", "Flux");
    }

    public IActionResult Publish(TitleData? movie)
    {
        return View(movie);
    }

    public async Task<ActionResult> RequestTitleMovie(string id)
    {
        try
        {


            ApiLib apiLib = new(ApiKey);
            TitleData? response = await apiLib.TitleAsync(id, Language.fr);
            return RedirectToAction("Publish", "Flux", response);
        }
        catch (HttpRequestException)
        {
            Console.WriteLine("An error occurred.");
        }
        catch (NotSupportedException)
        {
            Console.WriteLine("The content type is not supported.");
        }
        catch (JsonException)
        {
            Console.WriteLine("Invalid JSON.");
        }
        return RedirectToAction("MoviesList", "Flux");
    }
}