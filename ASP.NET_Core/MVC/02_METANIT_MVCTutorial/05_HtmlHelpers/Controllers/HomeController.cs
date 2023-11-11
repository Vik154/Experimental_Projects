using Microsoft.AspNetCore.Mvc;

namespace _05_HtmlHelpers.Controllers;

public class HomeController : Controller {
    public IActionResult Index() => View();

    [HttpGet]
    public IActionResult Create() => View();

    [HttpPost]
    public string Create(string name, int age) => $"{name} - {age}";
}
