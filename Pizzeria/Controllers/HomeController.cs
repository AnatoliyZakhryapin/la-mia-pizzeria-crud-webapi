using Microsoft.AspNetCore.Mvc;
using Pizzeria.Models;
using System.Diagnostics;

namespace Pizzeria.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            string currentController = ControllerContext.RouteData.Values["controller"].ToString();
            string currentAction = ControllerContext.RouteData.Values["action"].ToString();
            string currentPage = $"{currentController}/{currentAction}";

            ViewData["CurrentPage"] = currentPage;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
