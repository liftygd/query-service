using Application.Contracts.Base;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.Contracts.UserStatistics;

[SwaggerSchema("Запрос для отчетности по авторизации пользователей")]
public sealed class UserStatisticsRequest : Request
{
    [SwaggerSchema("Идентификатор пользователя")]
    public Guid UserId { get; init; }
}