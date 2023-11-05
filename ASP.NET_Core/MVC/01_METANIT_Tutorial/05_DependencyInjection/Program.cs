namespace _05_DependencyInjection;

/* � ASP.NET Core �� ����� �������� ����������� � ���������� ������� ���������� ���������:
 - ����� �������� Services ������� WebApplication (service locator)
 - ����� �������� RequestServices ��������� ������� HttpContext � ����������� middleware (service locator)
 - ����� ����������� ������
 - ����� �������� ������ Invoke ���������� middleware
 - ����� �������� Services ������� WebApplicationBuilder 
*/

public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddTransient<ITimeService, ShortTimeService>();
        builder.Services.AddTransient<TimeMessage>();

        var app = builder.Build();

        app.Map("", async context => await context.Response.WriteAsync("Hello World"));

        /// <summary> GetService<service>(): ���������� ��������� �������� ��� �������� �������,
        /// ������� ������������ ��� service. � ������ ���� � ���������� �������� ��� ������� �������
        /// �� ����������� �����������, �� ���������� �������� null</summary>
        app.Map("/GetService", async context => {
            var timeService = app.Services.GetService<ITimeService>();
            await context.Response.WriteAsync($"Time: {timeService?.GetTime()}");
        });

        /// <summary> ����������� ������� ����� ������������ ����� GetRequiredService() 
        /// �� ��� �����������, ��� ���� ������ �� ��������, �� ����� ���������� ����������:</summary>
        app.Map("/GetRequiredService", async context => {
            var timeService = app.Services.GetRequiredService<ITimeService>();
            await context.Response.WriteAsync($"Time: {timeService.GetTime()}");
        });

        /// <summary> HttpContext.RequestServices - ���, ��� �������� ������ HttpContext,
        /// �� ����� ������������ ��� ��������� �������� ��� �������� RequestServices </summary>
        app.Map("/RequestServices", async context => {
            var timeService = context.RequestServices.GetService<ITimeService>();
            await context.Response.WriteAsync($"Time: {timeService?.GetTime()}");
        });

        /// <summary> ������������� ������� TimeMessage </summary>
        app.Map("/TimeMessage", async context => {
            var timeMessage = context.RequestServices.GetService<TimeMessage>();
            context.Response.ContentType = "text/html;charset=utf-8";
            await context.Response.WriteAsync($"<h2>{timeMessage?.GetTime()}</h2>");
        });

        app.Run();
    }
}
