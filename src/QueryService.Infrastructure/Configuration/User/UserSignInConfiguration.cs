using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities.User;

namespace Infrastructure.Configuration;

public class UserSignInConfiguration : IEntityTypeConfiguration<EUserSignIn>
{
    public void Configure(EntityTypeBuilder<EUserSignIn> builder)
    {
        builder.HasKey(x => x.Id);
    }
}