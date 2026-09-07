using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QueryService.Domain.Entities.User;

namespace QueryService.Infrastructure.Configuration.User;

public class UserSignInConfiguration : IEntityTypeConfiguration<EUserSignIn>
{
    public void Configure(EntityTypeBuilder<EUserSignIn> builder)
    {
        builder.HasKey(x => x.Id);
    }
}