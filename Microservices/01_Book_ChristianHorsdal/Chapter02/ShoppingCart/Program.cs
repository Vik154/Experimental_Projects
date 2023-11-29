using Microsoft.AspNetCore.Server.Kestrel.Core;
using Nancy.Owin;
using ShoppingCart.ShoppingCart;

namespace ShoppingCart;

public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Services.AddScoped<IShoppingCartStore, ShoppingCartStore>();

        // If using Kestrel:
        builder.Services.Configure<KestrelServerOptions>(options => {
            options.AllowSynchronousIO = true;
        });

        // If using IIS:
        builder.Services.Configure<IISServerOptions>(options => {
            options.AllowSynchronousIO = true;
        });

        var app = builder.Build();

        app.UseRouting();
        app.UseOwin(func => func.UseNancy());

        app.MapGet("/", () => "Hello World!");

        app.Run();
    }
}
