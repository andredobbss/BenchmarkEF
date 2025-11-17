using BenchmarkEF.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BenchmarkEF.Infraestructure.Configurations;

internal sealed class FunctionaryDetailConfiguration : IEntityTypeConfiguration<FunctionaryDetail>
{
    public void Configure(EntityTypeBuilder<FunctionaryDetail> builder)
    {
        builder.ToTable("FunctionaryDetails");
        builder.HasKey(fd => fd.FunctionaryDetailId);
        builder.Property(fd => fd.Address)
            .IsRequired()
            .HasMaxLength(200);
        builder.Property(fd => fd.PhoneNumber)
            .IsRequired()
            .HasMaxLength(15);
        builder.Property(fd => fd.CPF)
            .IsRequired()
            .HasMaxLength(11);
        builder.Property(fd => fd.FunctionaryId)
           .IsRequired();

        builder.HasIndex(fd => fd.FunctionaryDetailId).HasDatabaseName("IX_FunctionaryDetails_FunctionaryDetailId");
    }
}
