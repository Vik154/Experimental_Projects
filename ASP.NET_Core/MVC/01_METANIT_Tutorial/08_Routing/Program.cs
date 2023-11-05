namespace _08_Routing;


public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);
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

        app.Run();
    }
}
