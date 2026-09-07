using Domain.Abstract;

namespace Infrastructure.Repository;

/// <summary>
/// Интерфейс для работы с базой данных.
/// </summary>
/// <typeparam name="TEntity">Тип сущности.</typeparam>
public interface IRepository<TEntity>
    where TEntity : IEntity
{
    /// <summary>
    /// Создание запроса с отслеживанием.
    /// </summary>
    IQueryable<TEntity> Tracking { get; }
    
    /// <summary>
    /// Создание запроса без отслеживания.
    /// </summary>
    IQueryable<TEntity> AsNoTracking { get; }
    
    /// <summary>
    /// Получение сущности по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор сущности.</param>
    /// <typeparam name="TId">Тип идентификатора.</typeparam>
    /// <returns>Сущность или NULL.</returns>
    Task<TEntity?> GetById<TId>(TId id);
    
    /// <summary>
    /// Добавление сущности в базу данных.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Результат операции.</returns>
    Task<bool> InsertAsync(TEntity entity);
    
    /// <summary>
    /// Обновление сущности в базе данных.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Результат операции.</returns>
    Task<bool> UpdateAsync(TEntity entity);
    
    /// <summary>
    /// Удалени сущности из базы данных.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Результат операции.</returns>
    Task<bool> DeleteAsync(TEntity entity);
    
    /// <summary>
    /// Сохранение данных в базу.
    /// </summary>
    /// <returns>Количество изменений.</returns>
    Task<int> SaveChangesAsync();
}