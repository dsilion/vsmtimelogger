using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Timelogger.Entities;

namespace Timelogger.Infrastructure.Configuration;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id).ValueGeneratedOnAdd();
        builder.Property(t => t.Name).HasMaxLength(200).IsRequired();
        builder.Property(t => t.DueDate).IsRequired();
        builder.HasMany(p=>p.ProjectTimelines)
            .WithOne(t => t.Project)
            .HasForeignKey(t => t.ProjectId);
    }
}