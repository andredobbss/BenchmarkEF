using BenchmarkEF.Domain.Entities.Enums;

namespace BenchmarkEF.Domain.Entities;

public sealed class Project
{
    public int ProjectId { get; set; }
    public string? ProjectName { get; set; }
    public string? Description { get; set; }
    public decimal Budget { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public DateTime EndDate { get; set; }
    public ProjectStatus Status { get; set; }

    // Navigation Properties
    public ICollection<FunctionaryProject>? FunctionaryProject { get; set; } = [];
}
