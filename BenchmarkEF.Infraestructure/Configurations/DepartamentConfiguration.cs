using BenchmarkEF.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BenchmarkEF.Infraestructure.Configurations;

internal sealed class DepartamentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ToTable("Departments");
        builder.HasKey(d => d.DepartmentId);
        builder.Property(d => d.DepartmentName)
               .IsRequired()
               .HasMaxLength(100);
        builder.Property(d => d.Description)
               .IsRequired()
               .HasMaxLength(200);

        builder.HasIndex(d => d.DepartmentId).HasDatabaseName("IX_Departaments_DepartmentId");
    }
}
