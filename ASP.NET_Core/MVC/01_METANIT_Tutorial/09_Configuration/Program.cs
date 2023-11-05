namespace _09_Configuration;

public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        /// <summary> AddInMemoryCollection() добавляет набор настроек в виде коллекции пар ключ-значение 
        /// public static IConfigurationBuilder AddInMemoryCollection(
        ///                 this IConfigurationBuilder configurationBuilder, 
        ///                 IEnumerable<KeyValuePair<string, string>> initialData)
        /// </summary>
        builder.Configuration.AddInMemoryCollection(new Dictionary<string, string?> {
            {"name1", "Timmy"},
            {"age1", "33"}
        });

        var app = builder.Build();


        // установка настроек конфигурации
        app.Configuration["name2"] = "Tom";
        app.Configuration["age2"] = "37";

        /// <summary> Получение данных конфигурации </summary>
        app.Map("/", async (context) => {
            string? name = app.Configuration["name1"];    // получение настроек конфигурации
            string? age = app.Configuration["age1"];      // получение настроек конфигурации
            await context.Response.WriteAsync($"{name} - {age}");
        });

        /// <summary> Получение конфигурации через Dependency Injection </ summary>
        app.Map("/get", (IConfiguration appConfig) => $"{appConfig["name2"]} - {appConfig["age2"]}");

        /// <summary> Получение конфигурации из JSON </ summary>
        builder.Configuration.AddJsonFile("config.json");
        app.Map("/json", (IConfiguration appConfig) => $"{appConfig["person:profile:name"]} - {appConfig["company:name"]}");

        /// <summary> Получение конфигурации из XML </ summary>
        builder.Configuration.AddXmlFile("config.xml");
        app.Map("/xml", (IConfiguration appConfig) => $"{appConfig["person:profile:name"]} - {appConfig["company:name"]}");

        /* Объединение конфигов из разных источников:
         
         builder.Configuration
            .AddJsonFile("config.json")
            .AddXmlFile("config.xml")
            .AddIniFile("config.ini")
            .AddInMemoryCollection(new Dictionary<string, string>
        {
            { "name", "Sam"},
            { "age", "32"}
        }); ;
         
         */

        app.Run();
    }
}
