using Application.Services.QueryManagerService;
using Domain.Contracts.Base;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Application.Runtime.Workers;

public class QueryWorker(
    IServiceScopeFactory scopeFactory)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = scopeFactory.CreateScope();

            var queryService = scope.ServiceProvider
                .GetRequiredService<IQueryManagerService>();

            var queries = await queryService.GetPendingQueries(new Request
            {
                Take = 10
            });

            foreach (var query in queries)
                await queryService.ExecuteQueryAsync(query);

            await Task.Delay(
                TimeSpan.FromSeconds(5),
                stoppingToken);
        }
    }
}