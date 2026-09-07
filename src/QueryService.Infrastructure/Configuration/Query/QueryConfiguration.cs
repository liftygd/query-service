using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QueryService.Domain.Entities.Query;

namespace QueryService.Infrastructure.Configuration.Query;

public class QueryConfiguration : IEntityTypeConfiguration<EQuery>
{
    public void Configure(EntityTypeBuilder<EQuery> builder)
    {
        builder.HasKey(e => e.Id);
    }
}