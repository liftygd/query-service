using Application.Contracts.Query;
using Domain.Entities.Query;

namespace Application.Runtime.Mediator;

/// <summary>
/// Медиатор для распределения запросов между обработчиками.
/// </summary>
public interface IQueryDispatcher
{
    /// <summary>
    /// Распределение запросов.
    /// </summary>
    /// <param name="query">Данные запроса.</param>
    /// <param name="request">Данные для фильтрации.</param>
    Task ExecuteAsync(QueryDispatchRequest request, EQuery query);
    
    /// <summary>
    /// Получение результата запроса.
    /// </summary>
    /// <param name="query">Данные запроса.</param>
    /// <typeparam name="TResponse">Тип данных запроса.</typeparam>
    /// <returns>Данные или NULL.</returns>
    Task<TResponse?> GetQueryResultAsync<TResponse>(EQuery query);
}