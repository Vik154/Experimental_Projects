using Notes.Application.Common.Mappings;
using Notes.Application.Interfaces;
using Notes.Application;
using Notes.Persistence;
using System.Reflection;

namespace Notes.WebApi;

public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        // builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblies(typeof(Program).Assembly));


        builder.Services.AddAutoMapper(config => {
            config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
            config.AddProfile(new AssemblyMappingProfile(typeof(INotesDbContext).Assembly));
        });

        builder.Services.AddApplication();
        builder.Services.AddPersistence(builder.Configuration);
        builder.Services.AddControllers();

        builder.Services.AddCors(opt => {
            opt.AddPolicy("AllowAll", policy => {
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
                policy.AllowAnyOrigin();
            });
        });

        var app = builder.Build();

        using (var scope = app.Services.CreateScope()) {

            var serviceProvider = scope.ServiceProvider;
            try {
                var context = serviceProvider.GetRequiredService<NotesDbContext>();
                DbInitializer.Initialize(context);
            }
            catch (Exception exp) {
            
            }
        }

       // app.UseExceptionHandler();
        app.UseRouting();
        app.UseHttpsRedirection();
        app.UseCors("AllowAll");

        // app.UseEndpoints(endp => endp.MapControllers());

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Note}");

        // app.MapGet("/", () => "Hello World!");

        app.Run();
    }
}

// test commit
// test commit
