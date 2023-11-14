using Microsoft.AspNetCore.Mvc;

namespace WebSite.Controllers;

public class ContactsController : Controller {
    public IActionResult Index() => View();
}
