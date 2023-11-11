namespace _08_MetaData;

public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddMvc();

        var app = builder.Build();

        app.MapControllerRoute("def", "{controller=Home}/{action=Create}");

        app.Run();
    }
}
