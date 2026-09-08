using Application.Contracts.Query;
using Domain.Contracts.Base;

namespace Application.Services.QueryManagerService;

/// <summary>
/// Интерфейс по работе с запросами.
/// </summary>
public interface IQueryManagerService
{
    /// <summary>
    /// Выполнение запроса.
    /// </summary>
    /// <param name="queryDispatchRequest">Данные о запросе.</param>
    /// <returns>Результат операции.</returns>
    Task<bool> ExecuteQueryAsync(QueryDispatchRequest queryDispatchRequest);

    /// <summary>
    /// Создание запроса.
    /// </summary>
    /// <param name="queryDispatchRequest">Данные о запросе.</param>
    /// <returns>Идентификатор запроса.</returns>
    Task<Guid> CreateQueryAsync(QueryDispatchRequest queryDispatchRequest);
    
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
    Task<QueryInfoResponse<TResponse>?> GetQueryInfoAsync<TResponse>(QueryInfoRequest queryInfoRequest)
        where TResponse : class;
}