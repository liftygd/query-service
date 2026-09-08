using Application.Contracts.Query;
using Application.Options;
using Application.Runtime.Mediator;
using Application.Services.ClockService;
using Domain.Contracts.Base;
using Domain.Entities.Query;
using Domain.Enums;
using Infrastructure.Extensions;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.QueryService;

public class QueryService(
    IRepository<EQuery> eQueryRepository,
    IQueryDispatcher dispatcher,
    IClockService clockService,
    QueryOptions queryOptions) 
    : IQueryService
{
    public async Task<bool> ExecuteQueryAsync(QueryDispatchRequest queryDispatchRequest)
    {
        var existingQuery = await (from eQuery in eQueryRepository.AsNoTracking
                                    where eQuery.Id == queryDispatchRequest.QueryId
                                        && eQuery.QueryType == queryDispatchRequest.QueryType
                                        && eQuery.State == QueryState.Pending
                                    select eQuery)
                                    .FirstOrDefaultAsync();

        if (existingQuery == null)
            throw new ApplicationException($"Не найден активный запрос с идентификатором {queryDispatchRequest.QueryId}.");
        
        await dispatcher.ExecuteAsync(queryDispatchRequest, existingQuery);
        return true;
    }

    public async Task<List<QueryDispatchRequest>> GetPendingQueries(Request request)
    {
        var threshold = clockService.UtcNow.Subtract(
            TimeSpan.FromMilliseconds(queryOptions.ProcessingDurationMS));
        
        var pendingQueries = await (from eQuery in eQueryRepository.AsNoTracking
                                                    where eQuery.State == QueryState.Pending
                                                        && eQuery.CreatedAt <= threshold
                                                    select new QueryDispatchRequest
                                                    {
                                                        QueryId = eQuery.Id,
                                                        QueryType = eQuery.QueryType
                                                    })
                                                    .TakeAndSkip(request)
                                                    .ToListAsync();
        
        return pendingQueries;
    }

    public async Task<QueryInfoResponse<TResponse>?> GetQueryInfoAsync<TResponse>(QueryInfoRequest queryInfoRequest)
    {
        var existingQuery = await (from eQuery in eQueryRepository.AsNoTracking
                where eQuery.Id == queryInfoRequest.QueryId
                select eQuery)
            .FirstOrDefaultAsync();

        if (existingQuery == null)
            throw new ApplicationException($"Не найден активный запрос с идентификатором {queryInfoRequest.QueryId}.");

        var data = await dispatcher.GetQueryResultAsync<TResponse?>(existingQuery);
        
        // Высчитываем прогресс.
        var elapsed = clockService.UtcNow - existingQuery.CreatedAt;
        var percentage = (int) Math.Clamp(
            Math.Floor(elapsed.TotalMilliseconds / queryOptions.ProcessingDurationMS),
            0,
            100);

        return new QueryInfoResponse<TResponse>
        {
            QueryId = existingQuery.Id,
            Percentage = percentage,
            Result = data,
        };
    }
}