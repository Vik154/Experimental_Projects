namespace _07_DependencyInjection;


public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        /// <summary> Регистрация для одной зависимости нескольких типов </summary>
        builder.Services.AddTransient<IHelloService, RuHelloService>();
        builder.Services.AddTransient<IHelloService, EnHelloService>();

        var app = builder.Build();

        app.UseMiddleware<HelloMiddleware>();

        app.Run();
    }
}
