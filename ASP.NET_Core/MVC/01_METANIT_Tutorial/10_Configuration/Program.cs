namespace _10_Configuration;

public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        builder.Configuration.AddTextFile("config.txt");

        app.Map("/", (IConfiguration appConfig) => $"{appConfig["name"]} - {appConfig["age"]}");

        app.Run();
    }
}
