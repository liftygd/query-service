using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Application.Options;
using Application.Runtime.Mediator;
using Application.Runtime.MIddleware;
using Application.Runtime.Workers;
using Application.Services.ClockService;
using Application.Services.QueryService;
using Application.Services.UserService;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<QueryOptions>(configuration.GetSection(QueryOptions.SectionName));

        services.AddScoped<IQueryDispatcher, QueryDispatcher>();
        
        services.AddScoped<IClockService, ClockService>();
        services.AddScoped<IQueryService, QueryService>();
        services.AddScoped<IUserService, UserService>();

        services.AddHostedService<QueryWorker>();
        
        return services;
    }

    public static IApplicationBuilder UseCustomExceptionHandling(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
        return app;
    }
}