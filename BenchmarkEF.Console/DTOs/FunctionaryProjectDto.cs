using BenchmarkEF.Domain.Entities.Enums;

namespace BenchmarkEF.Console.DTOs;

public sealed class FunctionaryProjectDto
{
    public string? FunctionaryName { get; set; }
    public string? Position { get; set; }
    public string? DepartmentName { get; set; }
    public string? ProjectName { get; set; }
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public DateTime EndDate { get; set; }
    public ProjectStatus Status { get; set; }
}
