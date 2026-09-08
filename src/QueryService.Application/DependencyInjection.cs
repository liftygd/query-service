using Application.Contracts.Query;
using Application.Contracts.Report.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Application.Options;
using Application.Runtime.Mediator;
using Application.Runtime.MIddleware;
using Application.Runtime.Workers;
using Application.Services.ClockService;
using Application.Services.QueryManagerService;
using Application.Services.ReportService;
using Application.Services.UserService;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<QueryOptions>(configuration.GetSection(QueryOptions.SectionName));

        services.AddScoped<IQueryHandler, UserStatisticsHandler>();
        services.AddScoped<IQueryDispatcher, QueryDispatcher>();
        
        services.AddScoped<IClockService, ClockService>();
        services.AddScoped<IQueryManagerService, QueryManagerService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IReportService, ReportService>();

        services.AddHostedService<QueryWorker>();
        
        return services;
    }

    public static IApplicationBuilder UseCustomExceptionHandling(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
        return app;
    }
}