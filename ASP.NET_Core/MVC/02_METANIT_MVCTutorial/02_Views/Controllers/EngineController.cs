using Microsoft.AspNetCore.Mvc;

namespace _02_Views.Controllers;

public class EngineController : Controller {
    public ViewResult Index() => View();
    public ViewResult About() => View("About");
    public ViewResult Contact() => View();
}
