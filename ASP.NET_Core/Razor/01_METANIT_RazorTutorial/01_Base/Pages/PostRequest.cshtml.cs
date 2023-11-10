using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _01_Base.Pages;

[IgnoreAntiforgeryToken]
public class PostRequestModel : PageModel {
    public string Message { get; private set; } = "";
    public void OnGet() => Message = "������� ���� ���";

    // public void OnPost(string name, int age) => Message = $"���� ���: {name}. ��� �������: {age}";
    
    // public void OnPost(string username) => Message = $"���� ���: {username}";
}
