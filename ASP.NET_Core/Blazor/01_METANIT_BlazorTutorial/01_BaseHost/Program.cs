using _01_Base;

namespace _01_BaseHost;

public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddRazorPages();   // добавляем поддержку Razor Pages

        var app = builder.Build();

        // app.UseBlazorFrameworkFiles() позволяет отправлять файлы фреймворка Blazor WebAssembly
        app.UseBlazorFrameworkFiles();
        app.UseStaticFiles();

        // app.MapFallbackToFile("index.html") устанавливает в качестве страницы по умолчанию
        // файл index.html, который берется из проекта Blazor WebAssembly.
        // app.MapFallbackToFile("index.html");

        // Теперь по умолчанию клиенту будет отправляться содержимое, сгенерированное из страницы "_Host.cshtml".
        app.MapFallbackToPage("/_Host");

        app.Run();
    }
}
