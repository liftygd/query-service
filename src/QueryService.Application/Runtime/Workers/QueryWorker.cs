using Application.Options;
using Application.Services.QueryManagerService;
using Domain.Contracts.Base;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

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
            var options = scope.ServiceProvider
                .GetRequiredService<IOptions<QueryOptions>>();

            var queries = await queryService.GetPendingQueries(new Request
            {
                Take = options.Value.WorkerBatchSize
            });

            foreach (var query in queries)
                await queryService.ExecuteQueryAsync(query);

            await Task.Delay(
                TimeSpan.FromSeconds(options.Value.WorkerPollIntervalSeconds),
                stoppingToken);
        }
    }
}