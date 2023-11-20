using Microsoft.AspNetCore.Mvc;
using RunGroopWebApp.Extensions;
using RunGroopWebApp.Models;
using System.Diagnostics;

namespace RunGroopWebApp.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger) {
            _logger = logger;
        }


        public IActionResult Index() {
            ViewBag.img = ImageConverter.ImageToByteArray($"{Directory.GetCurrentDirectory()}/wwwroot/img/running.webp");
            return View();
        }

        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
