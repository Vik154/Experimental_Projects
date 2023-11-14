using Microsoft.AspNetCore.Mvc;
using WebSite.Models;

namespace WebSite.Controllers;

public class ContactsController : Controller {
    public IActionResult Index() => View();

    [HttpPost]
    public IActionResult Check(Contact contact) {
        if (ModelState.IsValid) {
            return Redirect("/");
        }
        return View("Index");
    }
}
