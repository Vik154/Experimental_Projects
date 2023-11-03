namespace _03_UseMapWhenMiddleware;


public class ExampleErrorHandlingMiddleware {

    readonly RequestDelegate _next;
    public ExampleErrorHandlingMiddleware(RequestDelegate next) => _next = next;
    
    public async Task InvokeAsync(HttpContext context) {
        await _next.Invoke(context);
        if (context.Response.StatusCode == 403) {
            await context.Response.WriteAsync("Access Denied");
        }
        else if (context.Response.StatusCode == 404) {
            await context.Response.WriteAsync("Not Found");
        }
    }
}
