using Microsoft.AspNetCore.Mvc;

namespace _02_Views.Controllers;

/// <summary> Работа с формами </summary>
public class FormController : Controller {

    [HttpGet]
    public IActionResult Index() => View();
    
    [HttpPost]
    public string Index(string username, string password, int age, string comment) {
        return $"User Name: {username}   Password: {password}   Age: {age}  Comment: {comment}";
    }
}
