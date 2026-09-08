using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Repository;

namespace Infrastructure.Extensions;

/// <summary>
/// Методы расширения для работы с базой данных.
/// </summary>
public static class DatabaseExtensions
{
    /// <summary>
    /// Инициализация миграций.
    /// </summary>
    /// <param name="app">Приложение.</param>
    /// <typeparam name="T">Тип контекста.</typeparam>
    public static async Task InitializeDatabaseAsync<T>(this WebApplication app)
        where T : DbContext
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<T>();

        var pendingMigrations = await context.Database.GetPendingMigrationsAsync();

        if (pendingMigrations.Any())
            await context.Database.MigrateAsync();
    }

    /// <summary>
    /// Добавление репозиториев.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <returns>Коллекция сервисов.</returns>
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        return services;
    }
}