using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Domain.Contracts.Base;

namespace Application.Runtime.MIddleware;

public class ExceptionMiddleware(
    RequestDelegate next,
    ILogger<ExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next.Invoke(httpContext);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Произошла ошибка при выполнении запроса");
            
            httpContext.Response.StatusCode = ex switch
            {
                ApplicationException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

            await httpContext.Response.WriteAsJsonAsync(
                new Response<string>
                {
                    Data = "Операция завершилась с ошибкой",
                    Error = new Error
                    {
                        Message = ex.Message,
                        InnerError = ex.InnerException == null ? null :
                            new Error
                            {
                                Message = ex.InnerException.Message,
                            }
                    }
                });
        }
    }
}