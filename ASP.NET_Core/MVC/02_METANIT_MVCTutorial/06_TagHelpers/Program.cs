namespace _06_TagHelpers;


public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddMvc();

        var app = builder.Build();

        app.MapControllerRoute("default", "{controller=Home}/{action=Create}");
        
        app.Run();
    }
}
