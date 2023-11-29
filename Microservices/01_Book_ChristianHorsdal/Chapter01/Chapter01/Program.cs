using Microsoft.AspNetCore.Server.Kestrel.Core;
using Nancy.Owin;

namespace Chapter01;

public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        // If using Kestrel:
        builder.Services.Configure<KestrelServerOptions>(options => {
            options.AllowSynchronousIO = true;
        });

        // If using IIS:
        builder.Services.Configure<IISServerOptions>(options => {
            options.AllowSynchronousIO = true;
        });

        var app = builder.Build();

        app.UseOwin(buildFunc => {
            buildFunc(next => env => {
                Console.WriteLine("Got request");
                return next(env);
            });
            buildFunc.UseNancy();
        });

        app.UseRouting();
        app.MapControllerRoute("Nancy", "{controller=CurrentDateTime}");
        app.Run();
    }
}
