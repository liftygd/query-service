using Swashbuckle.AspNetCore.Annotations;

namespace Application.Contracts.Base;

[SwaggerSchema("Базовый класс для запросов")]
public class Request
{
    [SwaggerSchema("Фильтр по дате - Дата От")]
    public DateTime? DateFrom { get; init; }
    
    [SwaggerSchema("Фильтр по дате - Дата По")]
    public DateTime? DateTo { get; init; }
}