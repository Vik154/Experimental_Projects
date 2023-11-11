using Microsoft.AspNetCore.Mvc;

namespace _03_Routes.Controllers;

public class UsingAttributeController : Controller {

    [Route("UsingAttribute/Index")]
    public IActionResult Index() => Content("ASP.NET Core");

    [Route("About")]
    public IActionResult About() => Content("About site");

    [Route("homepage")]
    public string Index2() => "ASP.NET Core MVC 2";

    [Route("{name:minlength(3)}/{age:int}")]
    public string Person(string name, int age) => $"name={name} | age={age}";

    /// <summary> Использование префиксов </summary>
    [Route("main/test/{name}")]
    public string Test(string name) => name;

    [Route("main/{id:int}/{name:maxlength(10)}")]
    public string Test(int id, string name) => $" id={id} | name={name}";

    /// <summary> Множественные маршруты </summary>
    [Route("Rt")]       // сопоставляется с Home/Rt
    [Route("Rt2")]      // сопоставляется с Home/Rt2
    public string Rt() => "Index Page";
}
