using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Notes.Identity.Data;
using Notes.Identity.Models;
using System;

namespace Notes.Identity;

public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        var connectionString = builder.Configuration.GetConnectionString("DbConnection");
        builder.Services.AddDbContext<AuthDbContext>(options => options.UseSqlite(connectionString));

        builder.Services.AddIdentity<AppUser, IdentityRole>(config => {
            config.Password.RequiredLength = 4;
            config.Password.RequireDigit = false;
            config.Password.RequireNonAlphanumeric = false;
            config.Password.RequireUppercase = false;
        })
            .AddEntityFrameworkStores<AuthDbContext>()
            .AddDefaultTokenProviders();

        builder.Services.AddIdentityServer()
            .AddAspNetIdentity<AppUser>()
            .AddInMemoryApiResources(Configuration.ApiResources)
            .AddInMemoryIdentityResources(Configuration.IdentityResources)
            .AddInMemoryApiScopes(Configuration.ApiScopes)
            .AddInMemoryClients(Configuration.Clients)
            .AddDeveloperSigningCredential();

        builder.Services.ConfigureApplicationCookie(config => {
            config.Cookie.Name = "Notes.Identity.Cookie";
            config.LoginPath = "/Auth/Login";
            config.LogoutPath = "/Auth/Logout";
        });

        builder.Services.AddControllersWithViews();


        var app = builder.Build();

        using (var scope = app.Services.CreateScope()) {

            var serviceProvider = scope.ServiceProvider;
            try {
                var context = serviceProvider.GetRequiredService<AuthDbContext>();
                DbInitializer.Initialize(context);
            }
            catch (Exception exp) {
                var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
                logger.LogError(exp, "An error occurred while app initialization");
            }
        }
                
        app.UseRouting();
        app.UseIdentityServer();

        app.MapGet("/", () => "Hello World!");
        app.Run();
    }
}