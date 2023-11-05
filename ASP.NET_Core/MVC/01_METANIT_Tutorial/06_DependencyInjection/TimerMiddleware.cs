namespace _06_DependencyInjection;

/// <summary> компонент TimerMiddleware, который будет использовать сервис
/// TimeService для вывода времени на веб-страницу: </summary>
public class TimerMiddleware {
    RequestDelegate _next;

    public TimerMiddleware(RequestDelegate next) =>_next = next;

    public async Task InvokeAsync(HttpContext context, TimeService timeService) {
        if (context.Request.Path == "/time") {
            context.Response.ContentType = "text/html; charset=utf-8";
            await context.Response.WriteAsync($"Текущее время: {timeService?.Time}");
        }
        else {
            await _next.Invoke(context);
        }
    }
}
