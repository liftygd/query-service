using Application.Contracts.User.SignIn;
using Application.Services.UserService;
using Domain.Contracts.Base;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace QueryService.API.Controllers;

[ApiController]
[Route("user")]
public class UserController(
    IUserService userService)
    : ControllerBase
{
    [SwaggerResponse(200, "Успешная операция", typeof(Response<bool>))]
    [SwaggerResponse(400, "Ошибка операции", typeof(Response<string>))]
    [HttpPost("login")]
    public async Task<IActionResult> UserSignIn([FromBody] UserSignInRequest request)
        => Ok(await userService.LoginAsync(request));
}