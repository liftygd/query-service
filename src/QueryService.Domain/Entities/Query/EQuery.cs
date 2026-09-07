using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Domain.Enums;

namespace Domain.Entities.Query;

[Table("query")]
[Comment("Таблица с текущими запросами.")]
public class EQuery : Entity<Guid>
{
    [Column("query_type")]
    [Comment("Тип запроса.")]
    public QueryType QueryType { get; set; }
    
    [Column("query_state")]
    [Comment("Состояние запроса.")]
    public QueryState State { get; set; }
    
    [Column("message")]
    [Comment("Сообщение от запроса.")]
    public string? Message { get; set; }
    
    [Column("completed_time")]
    [Comment("Время, когда запрос завершился.")]
    public DateTime? CompletedTime { get; set; }
    
    public List<EQueryUserStatistics> QueryUserStatistics { get; set; }
}