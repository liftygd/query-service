using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Domain.Enums;
using Infrastructure.Extensions;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BaseDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("Default"), o =>
            {
                o.MapEnum<QueryType>("query_type");
                o.MapEnum<QueryState>("query_state");
            });
        });

        services.AddRepositories();
        
        return services;
    }
}