using Microsoft.AspNetCore.Mvc;

namespace _03_Routes.Controllers;

public class HomeController : Controller {

    /// <summary> Получение параметров маршрутов в контроллере </summary>
    public string Index() {
        var controller = RouteData.Values["controller"];
        var action = RouteData.Values["action"];
        return $"controller: {controller} | action: {action}";
    }
}
