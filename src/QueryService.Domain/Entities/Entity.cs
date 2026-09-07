using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using QueryService.Domain.Abstract;

namespace QueryService.Domain.Entities;

public class Entity<T> : IEntity<T>
{
    [Column("id")]
    [Comment("Идентификатор сущности")]
    public T Id { get; set; }
    
    [Column("created_at")]
    [Comment("Дата создания")]
    public DateTime CreatedAt { get; set; }
}