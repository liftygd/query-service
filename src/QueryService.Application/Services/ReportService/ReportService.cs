using Application.Contracts.Query;
using Application.Contracts.Report.Requests;
using Application.Services.QueryManagerService;
using Domain.Contracts.Base;
using Domain.Enums;

namespace Application.Services.ReportService;

public sealed class ReportService(
    IQueryManagerService queryManagerService)
    : IReportService
{
    public async Task<Response<Guid>> CreateUserStatisticsQueryAsync(UserStatisticsRequest userStatisticsRequest)
    {
        userStatisticsRequest.QueryType = QueryType.UserStatistics;
        var resultId = await queryManagerService.CreateQueryAsync(userStatisticsRequest);
        
        return new Response<Guid>
        {
            Data = resultId
        };
    }

    public async Task<Response<QueryInfoResponse<object>?>> GetQueryInfoAsync(QueryInfoRequest queryInfoRequest)
    {
        var queryResponse = await queryManagerService.GetQueryInfoAsync<object>(queryInfoRequest);
        return new Response<QueryInfoResponse<object>?>
        {
            Data = queryResponse
        };
    }
}