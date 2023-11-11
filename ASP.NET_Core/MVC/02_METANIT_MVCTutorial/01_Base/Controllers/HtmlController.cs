using Microsoft.AspNetCore.Mvc;

namespace _01_Base.Controllers;

public class HtmlController : Controller {
    public IActionResult Index() {
        return new HtmlResult("Hello world");
    }
}
