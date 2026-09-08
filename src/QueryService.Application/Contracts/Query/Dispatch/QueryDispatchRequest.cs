using Domain.Contracts.Base;
using Domain.Enums;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.Contracts.Query;

[SwaggerSchema("Обработка запроса")]
public class QueryDispatchRequest : Request
{
    [SwaggerSchema("Идентификатор запроса")]
    public Guid QueryId { get; init; }
    
    [SwaggerSchema("Тип запроса")]
    public QueryType QueryType { get; init; }
}