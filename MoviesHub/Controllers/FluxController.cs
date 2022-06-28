using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using MoviesHub.Models;
using MoviesHub_BLL.Services;
using Newtonsoft.Json;

namespace MoviesHub.Controllers;

public class FluxController : Controller
{
    private readonly UserService _userService;
    private readonly string _baseUrl = "https://imdb-api.com/fr/";
    private readonly string apiKey = "k_8nrxshx3";

    public FluxController(UserService userService)
    {
        _userService = userService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Create()
    {
        return View();
    }

    public async Task<ActionResult> MoviesList()
    {
        List<SearchResult> search = new();
        using HttpClient client = new();
        client.BaseAddress = new Uri(_baseUrl);
        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        try
        {
            SearchData? response = await client.GetFromJsonAsync<SearchData>($"API/Search/{apiKey}/leseigneur");
            Console.WriteLine(response?.Results);
            foreach (var movie in response.Results)
            {
                search.Add(movie);
            }
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
        return View();
    }
}