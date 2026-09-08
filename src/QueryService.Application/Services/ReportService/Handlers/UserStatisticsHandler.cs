using Application.Contracts.Query;
using Application.Contracts.Report.Data;
using Application.Contracts.Report.Requests;
using Application.Runtime.Mediator;
using Application.Services.ClockService;
using Domain.Entities.Query;
using Domain.Entities.User;
using Domain.Enums;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.ReportService;

public class UserStatisticsHandler(
    IRepository<EQueryUserStatistics> queryUserStatisticsRepository,
    IRepository<EQuery> queryRepository,
    IRepository<EUserSignIn> userSignInRepository,
    IClockService clockService) 
    : IQueryHandler<UserStatisticsData>
{
    public QueryType Type => QueryType.UserStatistics;
    
    public async Task CreateAsync(QueryDispatchRequest request)
    {
        var errorMessage = $"Ошибка при создании запроса типа {Type}.";
        if (request is not UserStatisticsRequest userStatisticsRequest)
            throw new ApplicationException(errorMessage, 
                new ApplicationException($"Передан некорректный тип модели - {request.GetType().Name}."));

        if (userStatisticsRequest.QueryId == null)
            throw new ApplicationException(errorMessage, 
                new ApplicationException("Не передан идентификатор очереди."));
        
        if (userStatisticsRequest.DateFrom == null)
            throw new ApplicationException(errorMessage, 
                new ApplicationException("Не передана дата начала отчета."));
        
        if (userStatisticsRequest.DateTo == null)
            throw new ApplicationException(errorMessage, 
                new ApplicationException("Не передана дата окончания отчета."));
        
        await queryUserStatisticsRepository.InsertAsync(new EQueryUserStatistics
        {
            QueryId = userStatisticsRequest.QueryId.Value,
            UserId = userStatisticsRequest.UserId,
            DateFrom = userStatisticsRequest.DateFrom.Value,
            DateTo = userStatisticsRequest.DateTo.Value
        });
    }

    public async Task ExecuteAsync(EQuery query)
    {
        var errorMessage = $"Ошибка при обработке запроса типа {Type}. Id: {query.Id}.";
        query.CompletedTime = clockService.UtcNow;

        try
        {
            var existingStatistic = await queryUserStatisticsRepository.AsNoTracking
                .Where(qs => qs.QueryId == query.Id)
                .FirstOrDefaultAsync();

            if (existingStatistic == null)
                throw new ApplicationException(errorMessage,
                    new ApplicationException("Не удалось найти данные запроса."));

            var signInCount = await userSignInRepository.AsNoTracking
                .Where(us => us.UserId == existingStatistic.UserId
                             && us.CreatedAt >= existingStatistic.DateFrom
                             && us.CreatedAt <= existingStatistic.DateTo)
                .CountAsync();

            existingStatistic.SignInCount = signInCount;
            await queryUserStatisticsRepository.UpdateAsync(existingStatistic);
            await queryUserStatisticsRepository.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            query.State = QueryState.Failed;
            query.Message = $"{ex.Message}. {ex.InnerException?.Message}";
            
            await queryRepository.UpdateAsync(query);
            await queryRepository.SaveChangesAsync();
        }
        
        query.State = QueryState.Completed;
        query.Message = "Операция прошла успешно";
        
        await queryRepository.UpdateAsync(query);
        await queryRepository.SaveChangesAsync();
    }

    public async Task<TResponse?> GetQueryResultAsync<TResponse>(EQuery query)
        where TResponse : class
    {
        var data = await GetTypeQueryResultAsync(query);
        return data as TResponse;
    }

    public async Task<UserStatisticsData?> GetTypeQueryResultAsync(EQuery query)
    {
        var existingStatistic = await queryUserStatisticsRepository.AsNoTracking
            .Where(qs => qs.QueryId == query.Id)
            .FirstOrDefaultAsync();

        if (existingStatistic == null
            || existingStatistic.SignInCount == null
            || existingStatistic.UserId == null)
            return null;

        return new UserStatisticsData
        {
            UserId = existingStatistic.UserId.Value,
            CountSignIn = existingStatistic.SignInCount.Value
        };
    }
}