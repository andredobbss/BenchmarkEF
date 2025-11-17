using BenchmarkEF.Domain.Entities;

namespace BenchmarkEF.Domain.Repositories;

public interface IBenchmarkEFRepository
{
   IQueryable<FunctionaryProjectDomain> GetFunctionariesWithProjectsEFLinq();
   IQueryable<FunctionaryProjectDomain> GetFunctionariesWithProjectsDapper();
   IQueryable<FunctionaryProjectDomain> GetFunctionariesWithProjectsEFSqlRaw();
   IQueryable<FunctionaryProjectDomain> GetFunctionariesWithProjectsDapperFileResource();
   IQueryable<FunctionaryProjectDomain> GetFunctionariesWithProjectsEFSqlRawFileResource();
   IQueryable<FunctionaryProjectDomain> GetFunctionariesWithProjectsDapperView();
   IQueryable<FunctionaryProjectDomain> GetFunctionariesWithProjectsEFSqlRawView();
}
