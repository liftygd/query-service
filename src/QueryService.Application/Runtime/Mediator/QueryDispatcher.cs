using Application.Contracts.Query;
using Domain.Entities.Query;
using Domain.Enums;

namespace Application.Runtime.Mediator;

public class QueryDispatcher : IQueryDispatcher
{
    private readonly IReadOnlyDictionary<QueryType, IQueryHandler> _handlers;

    public QueryDispatcher(IEnumerable<IQueryHandler> handlers)
    {
        _handlers = handlers.ToDictionary(x => x.Type);
    }

    public async Task CreateAsync(QueryDispatchRequest request)
    {
        if (!_handlers.TryGetValue(request.QueryType, out var handler))
            throw new InvalidOperationException($"Не найден обработчик для запроса типа '{request.QueryType}'");

        await handler.CreateAsync(request);
    }
    
    public async Task ExecuteAsync(EQuery query)
    {
        if (!_handlers.TryGetValue(query.QueryType, out var handler))
            throw new InvalidOperationException($"Не найден обработчик для запроса типа '{query.QueryType}'");

        await handler.ExecuteAsync(query);
    }

    public async Task<TResponse?> GetQueryResultAsync<TResponse>(EQuery query)
        where TResponse : class
    {
        if (!_handlers.TryGetValue(query.QueryType, out var handler))
            throw new InvalidOperationException($"Не найден обработчик для запроса типа '{query.QueryType}'");

        if (query.State != QueryState.Completed
            && query.State != QueryState.Failed)
            return null!;

        var data = await handler.GetQueryResultAsync<TResponse>(query);
        return data;
    }
}