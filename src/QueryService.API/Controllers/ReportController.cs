using Application.Contracts.Query;
using Application.Contracts.Report.Requests;
using Application.Services.ReportService;
using Domain.Contracts.Base;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace QueryService.API.Controllers;

[ApiController]
[Route("report")]
[SwaggerTag("Создание и чтение отчетов")]
public class ReportController(
    IReportService reportService)
    : ControllerBase
{
    [SwaggerResponse(200, "Успешная операция", typeof(Response<Guid>))]
    [SwaggerResponse(400, "Ошибка операции", typeof(Response<string>))]
    [HttpPost("user_statistics")]
    public async Task<IActionResult> CreateUserStatisticsReport([FromBody] UserStatisticsRequest userStatisticsRequest)
        => Ok(await reportService.CreateUserStatisticsQueryAsync(userStatisticsRequest));
    
    [SwaggerResponse(200, "Успешная операция", typeof(Response<QueryInfoResponse<object>?>))]
    [SwaggerResponse(400, "Ошибка операции", typeof(Response<string>))]
    [HttpGet("info")]
    public async Task<IActionResult> GetReportInfo([FromQuery] QueryInfoRequest queryInfoRequest)
        => Ok(await reportService.GetQueryInfoAsync(queryInfoRequest));
}