using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _01_Base.Pages;

public class DynamicModel : PageModel {

    public void OnGet() {
        ViewData["Message"] = "Список пользователей";
        ViewData["People"] = new List<string> { "Tom", "Sam", "Bob" };
    }
}
