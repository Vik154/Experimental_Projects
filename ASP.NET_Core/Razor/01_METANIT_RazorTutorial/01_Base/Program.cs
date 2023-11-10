using Microsoft.AspNetCore.Mvc;

namespace _01_Base;


public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        // добавляем в приложение сервисы Razor Pages
        // builder.Services.AddRazorPages();

        // добавляем в приложение сервисы Razor Pages
        builder.Services.AddRazorPages(options => {
            // отключаем глобально Antiforgery-токен
            options.Conventions.ConfigureFilter(new IgnoreAntiforgeryTokenAttribute());
        });

        var app = builder.Build();

        // добавляем поддержку маршрутизации для Razor Pages
        app.MapRazorPages();

        app.Run();
    }
}
