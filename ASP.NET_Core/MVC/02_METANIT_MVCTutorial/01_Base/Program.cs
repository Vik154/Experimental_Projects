namespace _01_Base;

public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();
        builder.Services.AddTransient<ITimeService, SimpleTimeService>(); // добавляем сервис ITimeService

        var app = builder.Build();

        app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Info}/{id?}");

        app.Run();
    }
}
