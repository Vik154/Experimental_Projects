namespace _05_DependencyInjection;

/* В ASP.NET Core мы можем получить добавленные в приложения сервисы различными способами:
 - Через свойство Services объекта WebApplication (service locator)
 - Через свойство RequestServices контекста запроса HttpContext в компонентах middleware (service locator)
 - Через конструктор класса
 - Через параметр метода Invoke компонента middleware
 - Через свойство Services объекта WebApplicationBuilder 
*/

public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddTransient<ITimeService, ShortTimeService>();
        builder.Services.AddTransient<TimeMessage>();

        var app = builder.Build();


        app.Map("", async context => await context.Response.WriteAsync("Hello World"));

        /// <summary> GetService<service>(): использует провайдер сервисов для создания объекта,
        /// который представляет тип service. В случае если в провайдере сервисов для данного сервиса
        /// не установлена зависимость, то возвращает значение null</summary>
        app.Map("/GetService", async context => {
            var timeService = app.Services.GetService<ITimeService>();
            await context.Response.WriteAsync($"Time: {timeService?.GetTime()}");
        });

        /// <summary> Аналогичный образом можно использовать метод GetRequiredService() 
        /// за тем исключением, что если сервис не добавлен, то метод генерирует исключение:</summary>
        app.Map("/GetRequiredService", async context => {
            var timeService = app.Services.GetRequiredService<ITimeService>();
            await context.Response.WriteAsync($"Time: {timeService.GetTime()}");
        });

        /// <summary> HttpContext.RequestServices - Там, где доступен объект HttpContext,
        /// мы можем использовать для получения сервисов его свойство RequestServices </summary>
        app.Map("/RequestServices", async context => {
            var timeService = context.RequestServices.GetService<ITimeService>();
            await context.Response.WriteAsync($"Time: {timeService?.GetTime()}");
        });

        /// <summary> Использование сервиса TimeMessage </summary>
        app.Map("/TimeMessage", async context => {
            var timeMessage = context.RequestServices.GetService<TimeMessage>();
            context.Response.ContentType = "text/html;charset=utf-8";
            await context.Response.WriteAsync($"<h2>{timeMessage?.GetTime()}</h2>");
        });

        /// <summary> Использование компонента middleware </summary>
        // app.UseMiddleware<TimeMessageMiddleware>();

        app.Run();
    }
}
