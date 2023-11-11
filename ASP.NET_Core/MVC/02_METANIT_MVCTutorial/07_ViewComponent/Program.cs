namespace _07_ViewComponent;

public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddMvc();

        var app = builder.Build();

        app.MapControllerRoute("def", "{controller=Home}/{action=Index}");

        app.Run();
    }
}
