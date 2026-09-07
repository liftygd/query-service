namespace QueryService.Domain.Abstract;

/// <summary>
/// Интерфейс базовой сущности.
/// </summary>
public interface IEntity
{
    /// <summary>
    /// Дата добавления сущности в базу данных.
    /// </summary>
    DateTime CreatedAt { get; set; }
}

/// <summary>
/// Интерфейс сущности с идентификатором.
/// </summary>
/// <typeparam name="TId">Тип идентификатора.</typeparam>
public interface IEntity<TId> : IEntity
{
    /// <summary>
    /// Идентификатор сущности.
    /// </summary>
    TId Id { get; }
}