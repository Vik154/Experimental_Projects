using _06_TagHelpers.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace _06_TagHelpers.Controllers;

public class HomeController : Controller {
    public IActionResult Index() => View();
    public IActionResult About() => Content("ABOUT INFO");
    public string Contacts() => "Contacts page";

    IEnumerable<Company> companies = new List<Company> {
            new Company( 1, "Apple"),
            new Company(2, "Samsung"),
            new Company(3, "Google")
    };

    public IActionResult Create() {
        ViewBag.Companies = new SelectList(companies, "Id", "Name");
        return View();
    }

    [HttpPost]
    public string Create(Product product) {
        Company? company = companies.FirstOrDefault(c => c.Id == product.CompanyId);
        return $"Добавлен новый элемент: {product.Name} ({company?.Name})";
    }
}
