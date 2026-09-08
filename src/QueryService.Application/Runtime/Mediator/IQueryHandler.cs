using Application.Contracts.Query;
using Domain.Entities.Query;
using Domain.Enums;

namespace Application.Runtime.Mediator;

/// <summary>
/// Интерфейс для специализированных обработчиков запросов.
/// </summary>
public interface IQueryHandler
{
    /// <summary>
    /// Тип запроса.
    /// </summary>
    QueryType Type { get; }
    
    /// <summary>
    /// Создание запроса.
    /// </summary>
    /// <param name="request">Данные запроса.</param>
    Task CreateAsync(QueryDispatchRequest request);

    /// <summary>
    /// Обработка запроса.
    /// </summary>
    /// <param name="query">Данные запроса.</param>
    Task ExecuteAsync(EQuery query);
    
    /// <summary>
    /// Получение результата запроса.
    /// </summary>
    /// <param name="query">Данные запроса.</param>
    /// <typeparam name="TResponse">Тип данных запроса.</typeparam>
    /// <returns>Данные или NULL.</returns>
    Task<TResponse?> GetQueryResultAsync<TResponse>(EQuery query)
        where TResponse : class;
}

public interface IQueryHandler<TResponseType> : IQueryHandler
    where TResponseType : class
{
    Task<TResponseType?> GetTypeQueryResultAsync(EQuery query);
}