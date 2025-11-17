using BenchmarkEF.Domain.Entities.Enums;

namespace BenchmarkEF.Domain.Entities;

public sealed class FunctionaryDetail
{
    public int FunctionaryDetailId { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public string? CPF { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public MaritalStatus MaritalStatus { get; set; }
    public Education Education { get; set; }

    // Foreign Key
    public int FunctionaryId { get; set; }

    // Navigation Property
    public Functionary? Functionary { get; set; }
}
