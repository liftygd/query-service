using Application.Contracts.Query;
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
    /// Обработка запроса.
    /// </summary>
    /// <param name="query">Данные запроса.</param>
    Task ExecuteAsync(QueryDispatch query);
}