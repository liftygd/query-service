using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Application.Options;
using Application.Runtime.Mediator;
using Application.Runtime.MIddleware;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<WorkerOptions>(configuration.GetSection(WorkerOptions.SectionName));

        services.AddScoped<IQueryDispatcher, QueryDispatcher>();
        
        return services;
    }

    public static IApplicationBuilder UseCustomExceptionHandling(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
        return app;
    }
}