using Application.Contracts.Query;
using Application.Contracts.Report.Data;
using Application.Contracts.Report.Requests;
using Application.Contracts.User.SignIn;
using Application.Options;
using Application.Runtime.Mediator;
using Application.Runtime.Workers;
using Application.Services.ClockService;
using Application.Services.QueryManagerService;
using Application.Services.ReportService;
using Application.Services.UserService;
using Domain.Entities.Query;
using Domain.Entities.User;
using Domain.Enums;
using Infrastructure;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;

namespace QueryService.Tests;

public sealed class QueryTests
{
    [Fact]
    public async Task UserStatisticsQuery_ReturnsSignInsForRequestedUser()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<BaseDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        await using var context = new BaseDbContext(options);
        await context.Database.EnsureCreatedAsync();

        var queryRepository = new Repository<EQuery>(context);
        var statisticsRepository = new Repository<EQueryUserStatistics>(context);
        var signInRepository = new Repository<EUserSignIn>(context);
        var clockService = new ClockService();
        var handler = new UserStatisticsHandler(
            statisticsRepository,
            queryRepository,
            signInRepository,
            clockService);
        
        var dispatcher = new QueryDispatcher(new IQueryHandler[] { handler });
        var queryManager = new QueryManagerService(
            queryRepository,
            dispatcher,
            clockService,
            Options.Create(new QueryOptions()));
        
        var reportService = new ReportService(queryManager);
        var userService = new UserService(signInRepository);

        // Act
        var userId = Guid.NewGuid();
        await userService.LoginAsync(new UserSignInRequest { UserId = userId });
        await userService.LoginAsync(new UserSignInRequest { UserId = userId });
        await userService.LoginAsync(new UserSignInRequest { UserId = Guid.NewGuid() });

        var queryResponse = await reportService.CreateUserStatisticsQueryAsync(
            new UserStatisticsRequest
            {
                UserId = userId,
                DateFrom = DateTime.UtcNow.AddMinutes(-1),
                DateTo = DateTime.UtcNow.AddMinutes(1)
            });

        var queryId = queryResponse.Data;
        context.ChangeTracker.Clear();

        await queryManager.ExecuteQueryAsync(new QueryDispatchRequest
        {
            QueryId = queryId,
            QueryType = QueryType.UserStatistics
        });

        var infoResponse = await reportService.GetQueryInfoAsync(new QueryInfoRequest
        {
            QueryId = queryId
        });
        var query = await queryRepository.GetById<Guid>(queryId);
        var result = Assert.IsType<UserStatisticsData>(infoResponse.Data?.Result);

        // Assert
        Assert.NotNull(query);
        Assert.Equal(QueryState.Completed, query.State);
        Assert.Equal(userId, result.UserId);
        Assert.Equal(2, result.CountSignIn);
    }

    [Fact]
    public async Task UserStatisticsQuery_CorrectQueryTiming()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<BaseDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        await using var context = new BaseDbContext(options);
        await context.Database.EnsureCreatedAsync();

        var queryRepository = new Repository<EQuery>(context);
        var statisticsRepository = new Repository<EQueryUserStatistics>(context);
        var signInRepository = new Repository<EUserSignIn>(context);
        var clockService = new ClockService();
        var handler = new UserStatisticsHandler(
            statisticsRepository,
            queryRepository,
            signInRepository,
            clockService);
        
        var dispatcher = new QueryDispatcher(new IQueryHandler[] { handler });
        var queryOptions = Options.Create(new QueryOptions
        {
            ProcessingDurationMS = 1000,
            WorkerBatchSize = 1,
            WorkerPollIntervalSeconds = 1
        });
        
        var queryManager = new QueryManagerService(
            queryRepository,
            dispatcher,
            clockService,
            queryOptions);
        
        var reportService = new ReportService(queryManager);
        
        // Act
        var userId = Guid.NewGuid();
        var queryResponse = await reportService.CreateUserStatisticsQueryAsync(
            new UserStatisticsRequest
            {
                UserId = userId,
                DateFrom = DateTime.UtcNow.AddMinutes(-1),
                DateTo = DateTime.UtcNow.AddMinutes(1)
            });

        var queryId = queryResponse.Data;
        context.ChangeTracker.Clear();
        
        await Task.Delay(queryOptions.Value.ProcessingDurationMS);
        
        var infoResponse = await reportService.GetQueryInfoAsync(new QueryInfoRequest
        {
            QueryId = queryId
        });
        
        var query = await queryRepository.GetById(queryId);

        // Assert
        Assert.NotNull(query);
        Assert.NotNull(infoResponse.Data);
        Assert.Equal(100, infoResponse.Data.Percentage);
    }
    
    [Fact]
    public async Task UserStatisticsQuery_CorrectDataAfterCompletion()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<BaseDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        await using var context = new BaseDbContext(options);
        await context.Database.EnsureCreatedAsync();

        var queryRepository = new Repository<EQuery>(context);
        var statisticsRepository = new Repository<EQueryUserStatistics>(context);
        var signInRepository = new Repository<EUserSignIn>(context);
        var clockService = new ClockService();
        var handler = new UserStatisticsHandler(
            statisticsRepository,
            queryRepository,
            signInRepository,
            clockService);
        
        var dispatcher = new QueryDispatcher(new IQueryHandler[] { handler });
        var queryOptions = Options.Create(new QueryOptions
        {
            ProcessingDurationMS = 1000,
            WorkerBatchSize = 1,
            WorkerPollIntervalSeconds = 1
        });
        
        var queryManager = new QueryManagerService(
            queryRepository,
            dispatcher,
            clockService,
            queryOptions);
        
        var reportService = new ReportService(queryManager);
        
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddSingleton<IQueryManagerService>(queryManager);
        serviceCollection.AddSingleton<IOptions<QueryOptions>>(queryOptions);
        var serviceProvider = serviceCollection.BuildServiceProvider();
        IServiceScopeFactory scopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();

        var queryWorker = new QueryWorker(scopeFactory);
        
        // Act
        var userId = Guid.NewGuid();
        var queryResponse = await reportService.CreateUserStatisticsQueryAsync(
            new UserStatisticsRequest
            {
                UserId = userId,
                DateFrom = DateTime.UtcNow.AddMinutes(-1),
                DateTo = DateTime.UtcNow.AddMinutes(1)
            });

        var queryId = queryResponse.Data;
        context.ChangeTracker.Clear();
        
        using var cts = new CancellationTokenSource();
        await queryWorker.StartAsync(cts.Token);
        await Task.Delay(queryOptions.Value.ProcessingDurationMS, cts.Token); 
        await queryWorker.StopAsync(CancellationToken.None);
        
        var infoResponse = await reportService.GetQueryInfoAsync(new QueryInfoRequest
        {
            QueryId = queryId
        });
        
        var query = await queryRepository.GetById(queryId);

        // Assert
        var result = Assert.IsType<UserStatisticsData>(infoResponse.Data?.Result);
        
        Assert.NotNull(query);
        Assert.NotNull(infoResponse.Data);
        Assert.Equal(100, infoResponse.Data.Percentage);
        Assert.Equal(QueryState.Completed, query.State);
        Assert.Equal(0, result.CountSignIn);
        Assert.Equal(userId, result.UserId);
    }
}