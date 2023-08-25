using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Timelogger.Entities;

namespace Timelogger.Infrastructure.Configuration;

public class ProjectTimelineConfiguration : IEntityTypeConfiguration<ProjectTimeline>
{
    public void Configure(EntityTypeBuilder<ProjectTimeline> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id).ValueGeneratedOnAdd();
        builder.Property(t => t.StartDatetime).IsRequired();
        builder.Property(t => t.EndDatetime).IsRequired();
    }
}