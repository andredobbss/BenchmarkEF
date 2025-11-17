using BenchmarkEF.Domain.Entities;
using BenchmarkEF.Domain.Repositories;
using BenchmarkEF.Infraestructure.Context;
using BenchmarkEF.Infraestructure.Resources;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BenchmarkEF.Infraestructure.Repositories;

public class BenchmarkEFRepository : IBenchmarkEFRepository, IDisposable
{
    private readonly BenchmarkEFContext _context;

    private readonly string _connectionString;
    public BenchmarkEFRepository()
    {
        _context = new BenchmarkEFContext();

        _connectionString = _context.Database.GetDbConnection().ConnectionString;
    }

    // ========= EF LINQ ==========
    public IQueryable<FunctionaryProjectDomain> GetFunctionariesWithProjectsEFLinq()
    {
        var query =
            from f in _context.Functionaries!
            join d in _context.Departments!
                on f.DepartmentId equals d.DepartmentId
            join fp in _context.FunctionaryProject!
                on f.FunctionaryId equals fp.FunctionaryId
            join p in _context.Projects!
                on fp.ProjectId equals p.ProjectId
            orderby p.ProjectId
            select new FunctionaryProjectDomain
            {
                FunctionaryName = f.FunctionaryName,
                Position = f.Position,
                DepartmentName = d.DepartmentName,
                ProjectName = p.ProjectName,
                Description = p.Description,
                StartDate = p.StartDate,
                UpdateDate = p.UpdateDate,
                EndDate = p.EndDate,
                Status = p.Status
            };

        return query;
    }

    // ========= DAPPER ==========
    public IQueryable<FunctionaryProjectDomain> GetFunctionariesWithProjectsDapper()
    {
        using var connection = new SqlConnection(_connectionString);

        const string sql = @"SELECT 
                              [f].[FunctionaryName], 
                              [f].[Position], 
                              [d].[DepartmentName], 
                              [p].[ProjectName], 
                              [p].[Description], 
                              [p].[Budget], 
                              [p].[StartDate],  
                              [p].[UpdateDate],
                              [p].[EndDate],	 
                              [p].[Status] 
                           FROM 
                              [Functionaries] AS [f]
                           INNER JOIN 
                              [Departments] AS [d] ON [f].[DepartmentId] = [d].[DepartmentId]
                           INNER JOIN 
                              [FunctionaryProject] AS [f0] ON [f].[FunctionaryId] = [f0].[FunctionaryId]
                           INNER JOIN 
                              [Projects] AS [p] ON [f0].[ProjectId] = [p].[ProjectId]
                           ORDER BY
                               [p].[ProjectId]";

        return connection.Query<FunctionaryProjectDomain>(sql).AsQueryable();
    }

    // ========= EF SQL RAW ==========
    public IQueryable<FunctionaryProjectDomain> GetFunctionariesWithProjectsEFSqlRaw()
    {
        const string sql = @"SELECT 
                               [f].[FunctionaryName], 
                               [f].[Position], 
                               [d].[DepartmentName], 
                               [p].[ProjectName], 
                               [p].[Description], 
                               [p].[Budget], 
                               [p].[StartDate],  
                               [p].[UpdateDate],
                               [p].[EndDate],	 
                               [p].[Status] 
                            FROM 
                               [Functionaries] AS [f]
                            INNER JOIN 
                               [Departments] AS [d] ON [f].[DepartmentId] = [d].[DepartmentId]
                            INNER JOIN 
                               [FunctionaryProject] AS [f0] ON [f].[FunctionaryId] = [f0].[FunctionaryId]
                            INNER JOIN 
                               [Projects] AS [p] ON [f0].[ProjectId] = [p].[ProjectId]
                            ORDER BY
                                [p].[ProjectId]";

        return _context.Database.SqlQueryRaw<FunctionaryProjectDomain>(sql).AsQueryable();
    }

    // ========= DAPPER – RESOURCE ==========
    public IQueryable<FunctionaryProjectDomain> GetFunctionariesWithProjectsDapperFileResource()
    {
        using var connection = new SqlConnection(_connectionString);

        var sql = BenchmarkEFResources.FunctionaryProject;

        return connection.Query<FunctionaryProjectDomain>(sql).AsQueryable();
    }

    // ========= EF SQL RAW – RESOURCE ==========
    public IQueryable<FunctionaryProjectDomain> GetFunctionariesWithProjectsEFSqlRawFileResource()
    {
        var sql = BenchmarkEFResources.FunctionaryProject;

        return _context.Database.SqlQueryRaw<FunctionaryProjectDomain>(sql).AsQueryable();
    }

    // ========= DAPPER – VIEW ==========
    public IQueryable<FunctionaryProjectDomain> GetFunctionariesWithProjectsDapperView()
    {
        using var connection = new SqlConnection(_connectionString);

        const string sql = "SELECT * FROM vw_FunctionaryProject ORDER BY ProjectId";

        return connection.Query<FunctionaryProjectDomain>(sql).AsQueryable();
    }

    // ========= EF SQL RAW – VIEW ==========
    public IQueryable<FunctionaryProjectDomain> GetFunctionariesWithProjectsEFSqlRawView()
    {
        const string sql = "SELECT * FROM vw_FunctionaryProject ORDER BY ProjectId";

        return _context.Database.SqlQueryRaw<FunctionaryProjectDomain>(sql).AsQueryable();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
