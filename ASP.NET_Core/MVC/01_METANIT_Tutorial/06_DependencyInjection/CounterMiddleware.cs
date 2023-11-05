namespace _06_DependencyInjection;

/// <summary> компонент middleware для работы с сервисами </summary>
public class CounterMiddleware {
    RequestDelegate _next;
    int i = 0; // счетчик запросов

    public CounterMiddleware(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(HttpContext httpContext, ICounter counter, CounterService counterService) {
        i++;
        httpContext.Response.ContentType = "text/html;charset=utf-8";
        await httpContext.Response.WriteAsync($"Запрос {i}; Counter: {counter.Value}; Service: {counterService.Counter.Value}");
    }
}
