using System.Text;

namespace _04_DependencyInjection;


public class Program {

    /// <summary> IServiceCollection - все встроенные сервисы </summary>
    static IServiceCollection Services { get; set; }

    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        /// <summary> Получение всех встроенных сервисов </summary>
        Services = builder.Services;    
        var app = builder.Build();

        app.Run(TestServices);
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
