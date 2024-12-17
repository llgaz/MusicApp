using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["WelcomeMessage"] = "Welcome to My Music Market!";
            return View();
        }
    }
}
