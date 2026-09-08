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

    public Task ExecuteAsync(QueryDispatchRequest request, EQuery query)
    {
        if (!_handlers.TryGetValue(query.QueryType, out var handler))
            throw new InvalidOperationException($"Не найден обработчик для запроса типа '{query.QueryType}'");

        return handler.ExecuteAsync(request, query);
    }

    public Task<TResponse?> GetQueryResultAsync<TResponse>(EQuery query)
    {
        if (!_handlers.TryGetValue(query.QueryType, out var handler))
            throw new InvalidOperationException($"Не найден обработчик для запроса типа '{query.QueryType}'");

        return handler.GetQueryResultAsync<TResponse>(query);
    }
}