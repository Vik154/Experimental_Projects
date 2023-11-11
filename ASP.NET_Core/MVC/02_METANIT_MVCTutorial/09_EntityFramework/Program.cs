using _09_EntityFramework.Models;
using Microsoft.EntityFrameworkCore;

namespace _09_EntityFramework;

public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        // получаем строку подключения из файла конфигурации
        string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

        // добавляем контекст ApplicationContext в качестве сервиса в приложение
        builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        app.MapDefaultControllerRoute();

        app.Run();
    }
}
