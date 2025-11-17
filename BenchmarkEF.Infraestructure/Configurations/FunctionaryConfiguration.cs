using BenchmarkEF.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BenchmarkEF.Infraestructure.Configurations;

internal sealed class FunctionaryConfiguration : IEntityTypeConfiguration<Functionary>
{
    public void Configure(EntityTypeBuilder<Functionary> builder)
    {
        builder.ToTable("Functionaries");
        builder.HasKey(f => f.FunctionaryId);
        builder.Property(f => f.FunctionaryName)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(f => f.Position)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(f => f.Salary)
            .HasPrecision(10, 2);
        builder. Property(f => f.DepartmentId)
            .IsRequired();

        builder.HasIndex(f => f.FunctionaryId).HasDatabaseName("IX_Functionaries_FunctionaryId");
    }
}
