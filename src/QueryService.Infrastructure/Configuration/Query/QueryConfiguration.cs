using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities.Query;

namespace Infrastructure.Configuration;

public class QueryConfiguration : IEntityTypeConfiguration<EQuery>
{
    public void Configure(EntityTypeBuilder<EQuery> builder)
    {
        builder.HasKey(e => e.Id);
    }
}