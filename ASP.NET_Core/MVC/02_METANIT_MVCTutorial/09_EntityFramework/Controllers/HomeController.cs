using _09_EntityFramework.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _09_EntityFramework.Controllers;

public class HomeController : Controller {

    ApplicationContext db;

    public HomeController(ApplicationContext context) => db = context;

    public async Task<IActionResult> Index() => View(await db.Users.ToListAsync());

    public IActionResult Create() => View();
    
    [HttpPost]
    public async Task<IActionResult> Create(User user) {
        db.Users.Add(user);
        await db.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}
