using _10_EntityFrameworkSort.Models;
using Microsoft.EntityFrameworkCore;

namespace _10_EntityFrameworkSort;


public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        string connection = "Server = (localdb)\\mssqllocaldb;Database = userstoredb;Trusted_Connection=true";
        builder.Services.AddDbContext<UsersContext>(options => options.UseSqlServer(connection));

        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        app.MapDefaultControllerRoute();

        app.Run();
    }
}
