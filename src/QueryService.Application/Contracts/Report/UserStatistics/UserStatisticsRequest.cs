using Application.Contracts.Query;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.Contracts.Report.Requests;

[SwaggerSchema("Запрос для отчетности по авторизации пользователей")]
public sealed class UserStatisticsRequest : QueryDispatchRequest
{
    [SwaggerSchema("Идентификатор пользователя")]
    public Guid UserId { get; init; }
}