using BenchmarkEF.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BenchmarkEF.Infraestructure.Configurations;

internal sealed class FunctionaryProjectConfiguration : IEntityTypeConfiguration<FunctionaryProject>
{
    public void Configure(EntityTypeBuilder<FunctionaryProject> builder)
    {
        builder.HasKey(fp => new { fp.FunctionaryId, fp.ProjectId });
    }
}
