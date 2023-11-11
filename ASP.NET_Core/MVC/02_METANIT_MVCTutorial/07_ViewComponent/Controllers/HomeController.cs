using Microsoft.AspNetCore.Mvc;

namespace _07_ViewComponent.Controllers;

public class HomeController : Controller {
    public IActionResult Index() => View();
}
