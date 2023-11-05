using _07_DependencyInjection.OneObject;

namespace _07_DependencyInjection;


public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        /// <summary> Регистрация для одной зависимости нескольких типов </summary>
        //builder.Services.AddTransient<IHelloService, RuHelloService>();
        //builder.Services.AddTransient<IHelloService, EnHelloService>();

        //var app = builder.Build();

        //app.UseMiddleware<HelloMiddleware>();

        /// <summary> Регистрация одного объекта для нескольких зависимостей </ summary>
        builder.Services.AddSingleton<IGenerator, ValueStorage>();
        builder.Services.AddSingleton<IReader, ValueStorage>();

        var app = builder.Build();

        app.UseMiddleware<GeneratorMiddleware>();
        app.UseMiddleware<ReaderMiddleware>();

        app.Run();
    }
}
