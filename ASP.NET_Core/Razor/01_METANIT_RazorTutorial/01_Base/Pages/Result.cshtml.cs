using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _01_Base.Pages;


public class ResultModel : PageModel {
    // public IActionResult OnGet() => Content("Hello World!");
    public IActionResult OnGet() => Page();
}
