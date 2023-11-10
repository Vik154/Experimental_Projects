using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _01_Base.Pages;

[IgnoreAntiforgeryToken]
public class BindingModel : PageModel {
    
    [BindProperty]
    public string Name { get; set; } = "";

    [BindProperty]
    public int Age { get; set; }
}
