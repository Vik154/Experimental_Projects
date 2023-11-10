using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _01_Base.Pages;

public class AboutModel : PageModel {
    public string Message { get; private set; } = "";
    public void OnGet() {
        Message = "Razor Pages About";
    }
}
