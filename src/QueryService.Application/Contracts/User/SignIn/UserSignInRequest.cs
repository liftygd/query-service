using Domain.Contracts.Base;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.Contracts.User.SignIn;

[SwaggerSchema("Запрос на авторизацию пользователя")]
public class UserSignInRequest : Request
{
    [SwaggerSchema("Идентификатор пользователя")]
    public Guid UserId { get; init; }
}