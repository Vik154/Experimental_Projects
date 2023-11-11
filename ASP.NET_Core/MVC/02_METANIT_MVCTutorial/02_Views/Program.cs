namespace _02_Views;

public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        // добавляем поддержку контроллеров с представлениями
        builder.Services.AddControllersWithViews();

        // внедряем сервис ITimeService
        builder.Services.AddTransient<ITimeService, SimpleTimeService>();

        var app = builder.Build();

        // устанавливаем сопоставление маршрутов с контроллерами
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");


        app.Run();
    }
}
