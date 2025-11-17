using BenchmarkEF.Domain.Repositories;
using BenchmarkEF.Infraestructure.Repositories;

namespace BenchmarkEF.Console.Services;

internal sealed class DatabaseValidatorService
{
    private readonly IDatabaseValidatorRepository _databaseValidatorRepository;
    internal DatabaseValidatorService()
    {
        _databaseValidatorRepository = new DatabaseValidatorRepository();
    }
    internal bool HasDatabase()
    {
        return _databaseValidatorRepository.HasDatabase();
    }
}
