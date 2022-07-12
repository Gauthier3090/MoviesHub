using Microsoft.AspNetCore.Mvc;

namespace MoviesWorld.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult NotFound404()
        {
            return View();
        }
    }
}
