using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QueryService.Domain.Entities.Query;

namespace QueryService.Infrastructure.Configuration.Query;

public class QueryUserStatisticsConfiguration : IEntityTypeConfiguration<EQueryUserStatistics>
{
    public void Configure(EntityTypeBuilder<EQueryUserStatistics> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasOne(x => x.Query)
            .WithMany(x => x.QueryUserStatistics)
            .HasForeignKey(x => x.QueryId);
    }
}