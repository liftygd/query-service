using Application.Contracts.Query;
using Application.Contracts.Report.Requests;
using Domain.Contracts.Base;

namespace Application.Services.ReportService;

/// <summary>
/// Интерфейс для взаимодействия с отчетами.
/// </summary>
public interface IReportService
{
    /// <summary>
    /// Создание запроса для получения отчета по авторизации пользователя.
    /// </summary>
    /// <param name="userStatisticsRequest">Запрос для отчета по статистике пользователя.</param>
    /// <returns>Идентификатор запроса.</returns>
    Task<Response<Guid>> CreateUserStatisticsQueryAsync(UserStatisticsRequest userStatisticsRequest);
    
    /// <summary>
    /// Получение информации о запросе.
    /// </summary>
    /// <param name="queryInfoRequest">Данные запроса.</param>
    /// <returns>Информации о запросе.</returns>
    Task<Response<QueryInfoResponse<object>?>> GetQueryInfoAsync(QueryInfoRequest queryInfoRequest);
}