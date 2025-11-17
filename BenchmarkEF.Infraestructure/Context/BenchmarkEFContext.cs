using BenchmarkEF.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BenchmarkEF.Infraestructure.Context;

public sealed class BenchmarkEFContext : DbContext
{
    public DbSet<Department>? Departments { get; set; } = null;
    public DbSet<Functionary>? Functionaries { get; set; } = null;
    public DbSet<FunctionaryDetail>? FunctionaryDetails { get; set; } = null;
    public DbSet<FunctionaryProject>? FunctionaryProject { get; set; } = null;
    public DbSet<Project>? Projects { get; set; } = null;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConnectionStringConfiguration.GetConnectionString())
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            .LogTo(Console.WriteLine,
                   new[] { DbLoggerCategory.Database.Command.Name },
                   Microsoft.Extensions.Logging.LogLevel.Information);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new Configurations.DepartamentConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.FunctionaryConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.FunctionaryDetailConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.FunctionaryProjectConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.ProjectConfiguration());
    }
}
