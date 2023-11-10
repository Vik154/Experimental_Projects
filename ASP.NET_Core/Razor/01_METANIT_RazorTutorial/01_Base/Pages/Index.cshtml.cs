using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _01_Base.Pages;

public class IndexModel : PageModel {
    public string Message { get; }

    public IndexModel() => Message = "Hello World";

    public string PrintTime() => DateTime.Now.ToShortTimeString();

    public void OnGet() { }
}
