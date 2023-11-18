using IdentityServer4.Models;

namespace Notes.Identity;

public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        var connectionString = builder.Configuration.GetConnectionString("DbConnection");
        // builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));


        builder.Services.AddIdentityServer()
            .AddInMemoryApiResources(Configuration.ApiResources)
            .AddInMemoryIdentityResources(Configuration.IdentityResources)
            .AddInMemoryApiScopes(Configuration.ApiScopes)
            .AddInMemoryClients(Configuration.Clients)
            .AddDeveloperSigningCredential();

        var app = builder.Build();
                
        app.UseRouting();
        app.UseIdentityServer();

        app.MapGet("/", () => "Hello World!");
        app.Run();
    }
}