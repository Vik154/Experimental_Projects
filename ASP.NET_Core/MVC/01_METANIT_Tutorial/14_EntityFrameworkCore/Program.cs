using Microsoft.EntityFrameworkCore;

namespace _14_EntityFrameworkCore;


public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        // получаем строку подключения из файла конфигурации
        string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

        // добавляем контекст ApplicationContext в качестве сервиса в приложение
        builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

        var app = builder.Build();

        // получение данных
        app.MapGet("/", (ApplicationContext db) => db.Users.ToList());

        app.Run();
    }
}
