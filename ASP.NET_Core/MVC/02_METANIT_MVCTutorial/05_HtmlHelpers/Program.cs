namespace _05_HtmlHelpers;

public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddMvc();

        var app = builder.Build();

        app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}
