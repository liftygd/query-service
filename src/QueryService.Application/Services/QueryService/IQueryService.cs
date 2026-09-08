using Application.Contracts.Query;
using Domain.Contracts.Base;

namespace Application.Services.QueryService;

/// <summary>
/// Интерфейс по работе с запросами.
/// </summary>
public interface IQueryService
{
    /// <summary>
    /// Выполнение запроса.
    /// </summary>
    /// <param name="queryDispatchRequest">Данные о запросе.</param>
    Task<bool> ExecuteQueryAsync(QueryDispatchRequest queryDispatchRequest);
    
    /// <summary>
    /// Получение списка запросов, которые ожидают обработки.
    /// </summary>
    /// <param name="request">Данные для фильтрации.</param>
    /// <returns>Список запросов.</returns>
    Task<List<QueryDispatchRequest>> GetPendingQueries(Request request);
    
    /// <summary>
    /// Получение информации о запросе.
    /// </summary>
    /// <param name="queryInfoRequest">Данные для фильтрации.</param>
    /// <typeparam name="TResponse">Тип данных запроса.</typeparam>
    /// <returns>Информация о запросе или NULL.</returns>
    Task<QueryInfoResponse<TResponse>?> GetQueryInfoAsync<TResponse>(QueryInfoRequest queryInfoRequest);
}