namespace _03_Routes;

public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        // добавляем поддержку контроллеров с представлениями
        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        // устанавливаем сопоставление маршрутов с контроллерами
        app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

        // устанавливаем сопоставление маршрутов с контроллерами
        app.MapControllerRoute(name: "about", pattern: "{controller=Home}/{action=About}/{id}");
        app.MapControllerRoute(name: "name_age", pattern: "{controller}/{action}/{name}/{age}");

        // Статические сегменты - https://localhost:7288/api/Home/Index/6
        app.MapControllerRoute(name: "default", pattern: "api/{controller}/{action}/{id?}");


        app.Run();
    }
}
