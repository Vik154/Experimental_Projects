namespace _03_UseMapWhenMiddleware;

/// <summary> Этот компонент в зависимости от строки запроса 
/// возвращает либо определенную строку, либо устанавливает код ошибки.
/// </summary>
public class ExampleRoutingMiddleware {
    public ExampleRoutingMiddleware(RequestDelegate _) { }

    public async Task InvokeAsync(HttpContext context) {
        string path = context.Request.Path;

        switch (path) {
            case "/index": await context.Response.WriteAsync("Home Page");  break;
            case "/about": await context.Response.WriteAsync("About Page"); break;
            default:             context.Response.StatusCode = 404;         break;
        }
    }
}
