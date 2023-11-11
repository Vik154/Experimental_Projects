using _04_Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace _04_Models.Controllers;

public class HomeController : Controller {

    List<Person> people = new List<Person> {
        new Person(1, "Tom", 37),
        new Person(2, "Bob", 41),
        new Person(3, "Sam", 28)
    };

    public IActionResult Index() => View(people);
}
