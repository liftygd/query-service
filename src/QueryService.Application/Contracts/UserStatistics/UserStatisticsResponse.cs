using Swashbuckle.AspNetCore.Annotations;

namespace Application.Contracts.UserStatistics;

[SwaggerSchema("Ответ по запросу для отчетности по авторизации пользователей")]
public sealed class UserStatisticsResponse
{
    [SwaggerSchema("Идентификатор запроса")]
    public Guid QueryId { get; set; }
}