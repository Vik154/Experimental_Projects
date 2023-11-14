using Site.Service;

namespace Site;

public class Program {

    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        // Подключение конфига appsettings.json секции "Project"
        builder.Configuration.Bind("Project", new Config());

        // Использование MVC
		builder.Services.AddControllersWithViews();

		var app = builder.Build();

        app.UseRouting();
        app.UseStaticFiles();

        app.MapControllerRoute("def", "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}
