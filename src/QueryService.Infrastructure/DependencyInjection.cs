using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QueryService.Domain.Enums;
using QueryService.Infrastructure.Repository;

namespace QueryService.Infrastructure;

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
        
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        
        return services;
    }
}