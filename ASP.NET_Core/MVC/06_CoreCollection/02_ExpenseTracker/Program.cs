using _02_ExpenseTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace _02_ExpenseTracker;

public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        // Добавление контекста БД
        builder.Services.AddDbContext<ApplicationDbContext>(options => {
            options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionSQL"));
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment()) {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Dashboard}/{action=Index}/{id?}");

        app.Run();
    }
}
