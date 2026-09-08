using Swashbuckle.AspNetCore.Annotations;

namespace Domain.Contracts.Base;

[SwaggerSchema("Базовый класс для ошибок")]
public class Error
{
    [SwaggerSchema("Сообщение ошибки")]
    public string Message { get; init; }
    
    [SwaggerSchema("Вложенная ошибка.")]
    public Error? InnerError { get; set; }
}