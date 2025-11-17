using BenchmarkDotNet.Attributes;
using BenchmarkEF.Console.DTOs;
using BenchmarkEF.Infraestructure.Context;
using BenchmarkEF.Infraestructure.Resources;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BenchmarkEF.Console.Services;

[RankColumn]
[MemoryDiagnoser]
public class BenchmarkEFService : IDisposable
{
    private readonly BenchmarkEFContext _context;

    private readonly string _connectionString;
    public BenchmarkEFService()
    {
        _context = new BenchmarkEFContext();

        _connectionString = _context.Database.GetDbConnection().ConnectionString;
    }

    // ========= EF LINQ ==========
    [Benchmark]
    public List<FunctionaryProjectDto> GetFunctionariesWithProjectsEFLinq()
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
            select new FunctionaryProjectDto
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

        return query.ToList();
    }

    // ========= DAPPER ==========
    [Benchmark]
    public List<FunctionaryProjectDto> GetFunctionariesWithProjectsDapper()
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

        return  connection.Query<FunctionaryProjectDto>(sql).ToList();
    }

    // ========= EF SQL RAW ==========
    [Benchmark]
    public List<FunctionaryProjectDto> GetFunctionariesWithProjectsEFSqlRaw()
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

        return _context.Database.SqlQueryRaw<FunctionaryProjectDto>(sql).ToList();
    }

    // ========= DAPPER – RESOURCE ==========
    [Benchmark]
    public List<FunctionaryProjectDto> GetFunctionariesWithProjectsDapperFileResource()
    {
        using var connection = new SqlConnection(_connectionString);

        var sql = BenchmarkEFResources.FunctionaryProject;

        return connection.Query<FunctionaryProjectDto>(sql).ToList();
    }

    // ========= EF SQL RAW – RESOURCE ==========
    [Benchmark]
    public List<FunctionaryProjectDto> GetFunctionariesWithProjectsEFSqlRawFileResource()
    {
        var sql = BenchmarkEFResources.FunctionaryProject;

        return _context.Database.SqlQueryRaw<FunctionaryProjectDto>(sql).ToList();
    }

    // ========= DAPPER – VIEW ==========
    [Benchmark]
    public List<FunctionaryProjectDto> GetFunctionariesWithProjectsDapperView()
    {
        using var connection = new SqlConnection(_connectionString);

        const string sql = "SELECT * FROM vw_FunctionaryProject ORDER BY ProjectId";

        return connection.Query<FunctionaryProjectDto>(sql).ToList();
    }

    // ========= EF SQL RAW – VIEW ==========
    [Benchmark]
    public List<FunctionaryProjectDto> GetFunctionariesWithProjectsEFSqlRawView()
    {
        const string sql = "SELECT * FROM vw_FunctionaryProject ORDER BY ProjectId";

        return _context.Database.SqlQueryRaw<FunctionaryProjectDto>(sql).ToList();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}

