using Microsoft.AspNetCore.Authorization;

namespace _15_AuthenticationAndAuthorization;


public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddAuthentication("Bearer")    // добавление сервисов аутентификации
            .AddJwtBearer();                            // подключение аутентификации с помощью jwt-токенов
        builder.Services.AddAuthorization();            // добавление сервисов авторизации

        var app = builder.Build();

        app.UseAuthentication();    // добавление middleware аутентификации 
        app.UseAuthorization();     // добавление middleware авторизации 

        app.Map("/hello", [Authorize] () => "Hello World!");
        app.Map("/", () => "Home Page");
        
        app.Run();
    }
}
