namespace BenchmarkEF.Domain.Entities;

public sealed class Functionary
{
    public int FunctionaryId { get; set; }
    public string? FunctionaryName { get; set; }
    public string? Position { get; set; }
    public decimal Salary { get; set; }
    public DateOnly HiringDate { get; set; }

    // Foreign Keys
    public int DepartmentId { get; set; }

    // Navigation Properties
    public Department? Department { get; set; }

    // Navigation Properties
    public FunctionaryDetail? FunctionaryDetail { get; set; }

    //Navigation Properties
    public ICollection<FunctionaryProject>? FunctionaryProject { get; set; } = [];

}
