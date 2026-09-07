using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QueryService.Domain.Entities.User;

[Table("user_sign_in")]
[Comment("Данные по авторизации пользователей.")]
public class EUserSignIn : Entity<long>
{
    [Column("user_id")]
    [Comment("Идентификатор пользователя.")]
    public Guid UserId { get; set; }
}