using Swashbuckle.AspNetCore.Annotations;

namespace Application.Contracts.Query;

[SwaggerSchema("Информации о запросе")]
public sealed class QueryInfoResponse<T>
{
    [SwaggerSchema("Идентификатор запроса")]
    public Guid QueryId { get; init; }
    
    [SwaggerSchema("Процент выполнения запроса")]
    public int Percentage { get; init; }
    
    [SwaggerSchema("Результат запроса")]
    public T? Result { get; init; }
}