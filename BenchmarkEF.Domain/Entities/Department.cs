namespace BenchmarkEF.Domain.Entities;

public sealed class Department
{
    public int DepartmentId { get; set; }
    public string? DepartmentName { get; set; }
    public string? Description { get; set; }

    // Navigation Properties
    public ICollection<Functionary>? Functionaries { get; set; } = [];
}
