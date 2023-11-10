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

        // добавляем сервис ITimeService
        builder.Services.AddTransient<ITimeService, SimpleTimeService>();

        var app = builder.Build();

        // добавляем поддержку маршрутизации для Razor Pages
        app.MapRazorPages();

        app.Run();
    }
}

/// <summary> Передача зависимостей на страницу </summary>
public interface ITimeService {
    string Time { get; }
}

/// <summary> Передача зависимостей на страницу </summary>
public class SimpleTimeService : ITimeService {
    public string Time => DateTime.Now.ToString("HH:mm:ss");
}
