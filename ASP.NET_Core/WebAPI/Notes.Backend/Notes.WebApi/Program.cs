using Notes.Application.Common.Mappings;
using Notes.Application.Interfaces;
using Notes.Application;
using Notes.Persistence;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;

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

        builder.Services.AddAuthentication(config => {
            config.DefaultAuthenticateScheme =
                JwtBearerDefaults.AuthenticationScheme;
            config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer("Bearer", opt => {
                opt.Authority = "http://localhost:44365";
                opt.Audience = "NotesWebAPI";
                opt.RequireHttpsMetadata = false;
            });

        // builder.Services.AddEndpointsApiExplorer()

        builder.Services.AddSwaggerGen(config => {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            config.IncludeXmlComments(xmlPath);
        });

        builder.Services.AddApiVersioning();
       

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

        app.UseSwagger();
        app.UseSwaggerUI(config => {
            config.RoutePrefix = string.Empty;
            config.SwaggerEndpoint("swagger/v1/swagger.json", "Notes API");
        });

        app.UseRouting();
        app.UseHttpsRedirection();
        app.UseCors("AllowAll");
        app.UseAuthentication();
        app.UseAuthorization();
        // app.UseApiVersioning();
        // app.UseEndpoints(endp => endp.MapControllers());

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Note}");

        // app.MapGet("/", () => "Hello World!");

        app.Run();
    }
}
