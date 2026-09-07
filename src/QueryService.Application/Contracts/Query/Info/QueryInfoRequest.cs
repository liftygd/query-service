using Application.Contracts.Base;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.Contracts.Query;

[SwaggerSchema("Получение информации о запросе")]
public sealed class QueryInfoRequest : Request
{
    [SwaggerSchema("Идентификатор запроса")]
    public Guid QueryId { get; init; }
}