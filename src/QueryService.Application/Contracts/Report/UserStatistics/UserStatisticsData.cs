using Application.Contracts.Query;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.Contracts.Report.Data;

[SwaggerSchema("Данные отчета по статистике пользователей")]
public sealed class UserStatisticsData
{
    [SwaggerSchema("Идентификатор пользователя")]
    public Guid UserId { get; set; }
    
    [SwaggerSchema("Количество авторизаций")]
    public int CountSignIn { get; set; }
}