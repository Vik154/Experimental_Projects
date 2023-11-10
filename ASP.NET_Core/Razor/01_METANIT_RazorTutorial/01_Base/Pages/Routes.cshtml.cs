using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _01_Base.Pages;

public class RoutesModel : PageModel {

    /// <summary> 
    /// Для получения параметров маршрута на странице Razor 
    /// и в ее модели применяется словарь RouteData.Values.
    /// С помощью ключа - названия параметра можно получить его значение: 
    /// </summary>
    public void OnGet() => Id = RouteData.Values["id"];
    
    public object? Id { get; private set; }
}
