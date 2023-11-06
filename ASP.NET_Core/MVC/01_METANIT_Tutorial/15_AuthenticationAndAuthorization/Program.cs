using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace _15_AuthenticationAndAuthorization;

/// <summary> класс AuthOptions - для описания некоторых настроек генерации токена </summary>
public class AuthOptions {
    public const string ISSUER = "MyAuthServer";        // издатель токена
    public const string AUDIENCE = "MyAuthClient";      // потребитель токена
    const string KEY = "mysupersecret_secretkey!123";   // ключ для шифрации
    public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
}

public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder();

        builder.Services.AddAuthorization();
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters {
                    // указывает, будет ли валидироваться издатель при валидации токена
                    ValidateIssuer = true,
                    // строка, представляющая издателя
                    ValidIssuer = AuthOptions.ISSUER,
                    // будет ли валидироваться потребитель токена
                    ValidateAudience = true,
                    // установка потребителя токена
                    ValidAudience = AuthOptions.AUDIENCE,
                    // будет ли валидироваться время существования
                    ValidateLifetime = true,
                    // установка ключа безопасности
                    IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                    // валидация ключа безопасности
                    ValidateIssuerSigningKey = true,
                };
            });
        var app = builder.Build();

        app.UseAuthentication();
        app.UseAuthorization();

        app.Map("/login/{username}", (string username) =>
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, username) };
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        });

        app.Map("/data", [Authorize] () => new { message = "Hello World!" });

        app.Run();
    }
}

//public class Program {
//    public static void Main(string[] args) {
//        var builder = WebApplication.CreateBuilder(args);

//        builder.Services.AddAuthentication("Bearer")    // добавление сервисов аутентификации
//            .AddJwtBearer();                            // подключение аутентификации с помощью jwt-токенов
//        builder.Services.AddAuthorization();            // добавление сервисов авторизации

//        var app = builder.Build();

//        app.UseAuthentication();    // добавление middleware аутентификации 
//        app.UseAuthorization();     // добавление middleware авторизации 

//        app.Map("/hello", [Authorize] () => "Hello World!");
//        app.Map("/", () => "Home Page");

//        app.Run();
//    }
//}
