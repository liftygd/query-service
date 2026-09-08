using Domain.Contracts.Base;

namespace Infrastructure.Extensions;

/// <summary>
/// Методы расширения для работы с запросами.
/// </summary>
public static class QueryExtensions
{
    /// <summary>
    /// Ограничение количества получаемых элементов.
    /// </summary>
    /// <param name="query">Запрос.</param>
    /// <param name="request">Данные для ограничения.</param>
    /// <typeparam name="TSource">Тип запроса.</typeparam>
    /// <returns>Запрос.</returns>
    public static IQueryable<TSource> TakeAndSkip<TSource>(this IQueryable<TSource> query, Request request)
    {
        if (request.Take != null)
        {
            var takeAmount = request.Take.Value;
            
            if (request.Skip != null)
            {
                var skipAmount = request.Skip.Value * takeAmount;
                query = query.Skip(skipAmount);
            }
            
            query = query.Take(takeAmount);
        }
        
        return query;
    }
}