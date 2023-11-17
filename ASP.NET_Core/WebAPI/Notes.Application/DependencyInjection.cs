using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;
// using Notes.WebApi;

namespace Notes.Application;

public static class DependencyInjection {
    public static IServiceCollection AddApplication(this IServiceCollection services) {
        services.AddMediatR(Assembly.GetExecutingAssembly());// - устарело походу
        //services.AddMediatR(cfg => {
        //    cfg.RegisterServicesFromAssembly(typeof(dummy).Assembly);
        //});

        return services;
    }
}
