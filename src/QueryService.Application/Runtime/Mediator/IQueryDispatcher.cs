using Application.Contracts.Query;

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
    Task ExecuteAsync(QueryDispatch query);
}