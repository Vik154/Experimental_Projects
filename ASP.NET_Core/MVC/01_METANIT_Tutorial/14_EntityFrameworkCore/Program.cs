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

        app.UseDefaultFiles();
        app.UseStaticFiles();

        // получение данных
        // app.MapGet("/", (ApplicationContext db) => db.Users.ToList());

        app.MapGet("/api/users", async (ApplicationContext db) => await db.Users.ToListAsync());

        app.MapGet("/api/users/{id:int}", async (int id, ApplicationContext db) =>
        {
            // получаем пользователя по id
            User? user = await db.Users.FirstOrDefaultAsync(u => u.Id == id);

            // если не найден, отправляем статусный код и сообщение об ошибке
            if (user == null) return Results.NotFound(new { message = "Пользователь не найден" });

            // если пользователь найден, отправляем его
            return Results.Json(user);
        });

        app.MapDelete("/api/users/{id:int}", async (int id, ApplicationContext db) =>
        {
            // получаем пользователя по id
            User? user = await db.Users.FirstOrDefaultAsync(u => u.Id == id);

            // если не найден, отправляем статусный код и сообщение об ошибке
            if (user == null) return Results.NotFound(new { message = "Пользователь не найден" });

            // если пользователь найден, удаляем его
            db.Users.Remove(user);
            await db.SaveChangesAsync();
            return Results.Json(user);
        });

        app.MapPost("/api/users", async (User user, ApplicationContext db) =>
        {
            // добавляем пользователя в массив
            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();
            return user;
        });

        app.MapPut("/api/users", async (User userData, ApplicationContext db) =>
        {
            // получаем пользователя по id
            var user = await db.Users.FirstOrDefaultAsync(u => u.Id == userData.Id);

            // если не найден, отправляем статусный код и сообщение об ошибке
            if (user == null) return Results.NotFound(new { message = "Пользователь не найден" });

            // если пользователь найден, изменяем его данные и отправляем обратно клиенту
            user.Age = userData.Age;
            user.Name = userData.Name;
            await db.SaveChangesAsync();
            return Results.Json(user);
        });

        app.Run();
    }
}
