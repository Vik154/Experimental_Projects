using System.Text;

namespace _04_DependencyInjection;


public class Program {

    /// <summary> IServiceCollection - все встроенные сервисы </summary>
    static IServiceCollection Services { get; set; }

    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        /// <summary> Получение всех встроенных сервисов </summary>
        Services = builder.Services;

        /// <summary> Добавление в коллекцию сервисов свои собственные
        /// система на место объектов интерфейса ITimeService будет 
        /// передавать экземпляры класса ShortTimeService.</summary>
        // 1 builder.Services.AddTransient<ITimeService, ShortTimeServices>();

        /// <summary> 2. Расширения для добавления сервисов </summary>
        builder.Services.AddTimeService();

        var app = builder.Build();

        /// <summary> Вывод всех встроенных сервисов фреймворка </summary>
        // app.Run(TestServices);

        /// <summary> Работа с сервисом ShortTimeServices </summary>
        // 1 app.Run(async context => {
        //    var timeService = app.Services.GetService<ITimeService>();
        //    await context.Response.WriteAsync($"Time: {timeService?.GetTime()}");
        //});

        /// <summary> 2. Расширения для добавления сервисов </summary>
        app.Run(async context => {
            var timeService = app.Services.GetService<TimeService>();
            context.Response.ContentType = "text/html; charset=utf-8";
            await context.Response.WriteAsync($"Текущее время: {timeService?.GetTime()}");
        });

        app.Run();

    }

    #region ВСТРОЕННЫЕ СЕРВИСЫ IServiceCollection

    /// <summary> Вывод всех встроенных сервисов фреймворка </summary>
    static async Task TestServices(HttpContext context) {
        var sb = new StringBuilder();
        sb.Append("<h1>Все сервисы</h1>");
        sb.Append("<table>");
        sb.Append("<tr><th>Тип</th><th>Lifetime</th><th>Реализация</th></tr>");
        foreach (var svc in Services) {
            sb.Append("<tr>");
            sb.Append($"<td>{svc.ServiceType.FullName}</td>");
            sb.Append($"<td>{svc.Lifetime}</td>");
            sb.Append($"<td>{svc.ImplementationType?.FullName}</td>");
            sb.Append("</tr>");
        }
        sb.Append("</table>");
        context.Response.ContentType = "text/html;charset=utf-8";
        await context.Response.WriteAsync(sb.ToString());
    }

    #endregion
}
