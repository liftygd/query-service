using Application.Contracts.Query;
using Domain.Entities.Query;

namespace Application.Runtime.Mediator;

/// <summary>
/// Медиатор для распределения запросов между обработчиками.
/// </summary>
public interface IQueryDispatcher
{
    /// <summary>
    /// Создание запроса.
    /// </summary>
    /// <param name="request">Данные запроса.</param>
    Task CreateAsync(QueryDispatchRequest request);
    
    /// <summary>
    /// Распределение запросов.
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