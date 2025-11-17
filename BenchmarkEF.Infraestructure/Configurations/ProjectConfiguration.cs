using BenchmarkEF.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BenchmarkEF.Infraestructure.Configurations;

internal sealed class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder .ToTable("Projects");
        builder.HasKey(p => p.ProjectId);
        builder.Property(p => p.ProjectName)
               .IsRequired()
               .HasMaxLength(100);
        builder.Property(p => p.Description)
               .IsRequired()
               .HasMaxLength(200);
        builder.Property(p => p.Budget)
               .HasPrecision(10, 2);

        builder.HasIndex(p => p.ProjectId).HasDatabaseName("IX_Projects_ProjectId");
    }
}
