using BenchmarkEF.Infraestructure;

namespace BenchmarkEF.Console.Services;

internal static class DataBaseService
{
    public static bool HasDatabase()
    {
        return DataBaseInfraestructure.HasDatabase();
    }
}
