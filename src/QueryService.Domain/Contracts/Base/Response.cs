using Swashbuckle.AspNetCore.Annotations;

namespace Domain.Contracts.Base;

[SwaggerSchema("Базовый класс для ответов")]
public class Response<T>
{
    [SwaggerSchema("Возвращаемые данные")]
    public T Data { get; init; }
    
    [SwaggerSchema("Информация об ошибке, если имеется")]
    public Error? Error { get; init; }
}