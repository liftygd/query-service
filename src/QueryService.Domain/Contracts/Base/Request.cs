using Swashbuckle.AspNetCore.Annotations;

namespace Domain.Contracts.Base;

[SwaggerSchema("Базовый класс для запросов")]
public class Request
{
    [SwaggerSchema("Фильтр по дате - Дата От")]
    public DateTime? DateFrom { get; init; }
    
    [SwaggerSchema("Фильтр по дате - Дата По")]
    public DateTime? DateTo { get; init; }
    
    [SwaggerSchema("Количество возвращаемых элементов")]
    public int? Take { get; init; }
    
    [SwaggerSchema("Количество пропущенных групп элементов")]
    public int? Skip { get; init; }
}