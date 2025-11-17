using BenchmarkEF.Domain.Repositories;
using BenchmarkEF.Infraestructure.Repositories;

namespace BenchmarkEF.Console.Services
{

    internal sealed class PersistenceService
    {
        private readonly IPersistenceRepository _persistenceRepository;
        internal PersistenceService()
        {
            _persistenceRepository = new PersistenceRepository();
        }

        internal void AddData()
        {
            _persistenceRepository.AddData();
        }
    }
}
