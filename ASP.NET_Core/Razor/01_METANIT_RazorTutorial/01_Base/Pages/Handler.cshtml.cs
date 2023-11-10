using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _01_Base.Pages;

public record class Person1(string Name, int Age);

public class HandlerModel : PageModel {
    
    // начальные данные - список людей
    List<Person1> people = new() {
            new Person1 ("Tom Smith", 23),
            new Person1 ("Sam Anderson", 23),
            new Person1 ("Bob Johnson", 25),
            new Person1 ("Tom Anderson", 25)
        };

    // отображаемые данные
    public List<Person1> DisplayedPeople { get; private set; } = new();

    public void OnGet() {
        DisplayedPeople = people;
    }

    public void OnGetByName(string name) {
        DisplayedPeople = people.Where(p => p.Name.Contains(name)).ToList();
    }
    public void OnGetByAge(int age) {
        DisplayedPeople = people.Where(p => p.Age == age).ToList();
    }
}
