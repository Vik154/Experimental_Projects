using Microsoft.AspNetCore.Mvc;

namespace _01_Base.Controllers;

public class HtmlController : Controller {
    public IActionResult Index() {
        return new HtmlResult("Hello world");
    }

    public IActionResult JsonResult1() => Json("Hello world");
    public IActionResult JsonResult2() => Json(new Person("Tommy", 33));
}
