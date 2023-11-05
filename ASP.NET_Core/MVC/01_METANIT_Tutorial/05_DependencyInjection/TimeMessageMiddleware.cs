namespace _05_DependencyInjection;

/// <summary> Метод Invoke/InvokeAsync компонентов middleware </summary>
public class TimeMessageMiddleware {

    private readonly RequestDelegate _next;

    public TimeMessageMiddleware(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(HttpContext context, ITimeService timeService) {
        context.Response.ContentType = "text/html;charset=utf-8";
        await context.Response.WriteAsync($"<h1>Time: {timeService.GetTime()}</h1>");
    }
}
