namespace _11_Logger;


public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        /*--------------------------------------------------------------------------*/
        // устанавливаем файл для логгирования
        builder.Logging.AddFile(Path.Combine(Directory.GetCurrentDirectory(), "logger.txt"));

        var app = builder.Build();

        app.Map("/FileLogger", async (context) => {
            app.Logger.LogInformation($"Path: {context.Request.Path}  Time:{DateTime.Now.ToLongTimeString()}");
            await context.Response.WriteAsync("Hello World!");
        });

        /*--------------------------------------------------------------------------*/

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

        /*--------------------------------------------------------------------------*/

        /// <summary> с помощью LoggerFactory.Create создается фабрика логгера в виде объекта ILoggerFactory </summary>
        ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
        ILogger logger = loggerFactory.CreateLogger<Program>();
        
        app.Map("/factory", async (context) => {
            logger.LogInformation($"Requested Path: {context.Request.Path}");
            await context.Response.WriteAsync("Hello World!");
        });

        /// <summary> Получение фабрики логгера через dependency injection </ summary>
        app.Map("/DI", (ILoggerFactory loggerFactory) => {

            // создаем логгер с категорией "MapLogger"
            ILogger logger = loggerFactory.CreateLogger("MapLogger");                           
            
            // логгируем некоторое сообщение
            logger.LogInformation($"Path: /hello   Time: {DateTime.Now.ToLongTimeString()}");
            return "Hello World!";
        });

        app.Run();
    }
}
