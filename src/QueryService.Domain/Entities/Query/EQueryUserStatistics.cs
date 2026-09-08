using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.Query;

[Table("query_user_statistics")]
[Comment("Данные для запросов по статистике пользователей.")]
public class EQueryUserStatistics : Entity<Guid>
{
    [Column("query_id")]
    [Comment("Идентификатор запроса.")]
    public Guid QueryId { get; set; }
    public EQuery Query { get; set; }
    
    [Column("user_id")]
    [Comment("Идентификатор пользователя.")]
    public Guid? UserId { get; set; }
    
    [Column("sign_in_count")]
    [Comment("Количество входов.")]
    public int? SignInCount { get; set; }
    
    [Column("date_from")]
    [Comment("Фильтр по дате - Дата От.")]
    public DateTime DateFrom { get; set; }
    
    [Column("date_to")]
    [Comment("Фильтр по дате - Дата По.")]
    public DateTime DateTo { get; set; }
}