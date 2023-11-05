namespace _11_Logger;


public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();


        app.Map("/", async (context) => {
            // пишем на консоль информацию
            app.Logger.LogInformation($"Processing request {context.Request.Path}");
            await context.Response.WriteAsync("Hello World!");
        });

        /// <summary> Получение логгера через внедрение зависимостей </ summary>
        app.Map("/hello", (ILogger<Program> logger) => {
            logger.LogInformation($"Path: /hello  Time: {DateTime.Now.ToLongTimeString()}");
            return "Hello World";
        });

        app.Map("/log", async (context) => {
            var path = context.Request.Path;
            app.Logger.LogCritical($"LogCritical {path}");
            app.Logger.LogError($"LogError {path}");
            app.Logger.LogInformation($"LogInformation {path}");
            app.Logger.LogWarning($"LogWarning {path}");

            await context.Response.WriteAsync("Hello World!");
        });

        app.Run();
    }
}
