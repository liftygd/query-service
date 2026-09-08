using Application.Contracts.User.SignIn;

namespace Application.Services.UserService;

/// <summary>
/// Интерфейс по работе с пользователями.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Запрос на авторизацию.
    /// </summary>
    /// <param name="signInRequest">Данные для авторизации.</param>
    /// <returns>Результат операции.</returns>
    Task<bool> LoginAsync(UserSignInRequest signInRequest);
}