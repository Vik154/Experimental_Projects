using System.Diagnostics.CodeAnalysis;

namespace _08_Routing;


public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        /// <summary> проецируем класс SecretCodeConstraint на inline-ограничение secretcode </summary>
        builder.Services.Configure<RouteOptions>(options =>
                        options.ConstraintMap.Add("secretcode", typeof(SecretCodeConstraint)));

        // альтернативное добавление класса ограничения
        // builder.Services.AddRouting(options => options.ConstraintMap.Add("secretcode", typeof(SecretConstraint)));

        /// <summary> Передача зависимостей в конечные точки </ summary>
        builder.Services.AddTransient<TimeService>();   // Добавляем сервис

        var app = builder.Build();

        /// <summary> Метод Map
        /// Самым простым способом определения конечной точки в приложении является метод Map,
        /// который реализован как метод расширения для типа IEndpointRouteBuilder.
        /// Он добавляет конечные точки для обработки запросов типа GET. 
        /// Данный метод имеет три версии:
        /// public static RouteHandlerBuilder Map (this IEndpointRouteBuilder endpoints, RoutePattern pattern, Delegate handler);
        /// public static IEndpointConventionBuilder Map(this IEndpointRouteBuilder endpoints, string pattern, RequestDelegate requestDelegate);
        /// public static RouteHandlerBuilder Map(this IEndpointRouteBuilder endpoints, string pattern, Delegate handler);
        /// </summary>
        app.Map("/", () => "Index Page");
        app.Map("/about", () => "About Page");
        app.Map("/contact", () => "Contacts Page");
        app.Map("/user", () => new { Name = "Tom", Age = 37 });
        app.Map("/info", async (c) => { await c.Response.WriteAsync("Info Page"); });

        /// <summary> Получение всех маршрутов приложения </summary>
        app.MapGet("/routes", (IEnumerable<EndpointDataSource> endpointSources) =>
            string.Join("\n", endpointSources.SelectMany(source => source.Endpoints)));

        /// <summary> Параметры маршрута => {название_параметра} </summary>
        app.Map("/users/{id}/{name}", (string id, string name) => $"User Id: {id} \nUser Name: {name}");
        app.Map("/users/{id}", (string id) => $"User id == {id}");
        app.Map("/users", () => "Users page");

        /// <summary> Необязательные параметры маршрута </summary>
        app.Map("param/{id?}", (string? id) => $"Param id == {id ?? "null"}");

        /// <summary> Значения параметров по умолчанию </summary>
        app.Map("test/{controller=Home}/{action=Page}/{id?}",
                (string controller, string action, string? id) => 
                    $"Controller: {controller} \nAction: {action} \nId: {id}");

        /// <summary> Передача произвольного количества параметров в запросе </summary>
        /* Мы можем обозначить любое количество сегментов в запросе, чтобы не быть жестко 
         * привязанным к числу сегментов с помощью параметра со знаком * ("звездочка") 
         * или ** (две звездочки) (это так называемый catchall-параметр): */
        app.Map("all/{**info}", (string info) => $"info {info}");

        /// <summary> Ограничения в маршрутах </summary>
        app.Map("constraint/{id:int}", (int id) => $"ID == {id}");
        
        app.Map("/constr/{name:alpha:minlength(2)}/{age:int:range(1, 110)}",
                (string name, int age) => $"User Age: {age} \nUser Name:{name}");

        app.Map("/phonebook/{phone:regex(^7-\\d{{3}}-\\d{{3}}-\\d{{4}}$)}/", (string phone) => $"Phone: {phone}");

        /*------------------------------------------------------------------------------------*/
        
        /// <summary> кастомные ограничения маршрутов </summary>
        app.Map("/custom/{name}/{token:secretcode(123456)}/",
                (string name, int token) => $"Name: {name} \nToken: {token}");

        /*------------------------------------------------------------------------------------*/

        /// <summary> Передача зависимостей в конечные точки </ summary>
        app.Map("/time", (TimeService timeService) => $"Time: {timeService.Time}");


        app.Run();
    }
}


// сервис
public class TimeService {
    public string Time => DateTime.Now.ToLongTimeString();
}
