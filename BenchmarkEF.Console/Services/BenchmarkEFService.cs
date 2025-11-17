using AutoMapper;
using BenchmarkDotNet.Attributes;
using BenchmarkEF.Console.DTOs;
using BenchmarkEF.Console.Mappings;
using BenchmarkEF.Domain.Repositories;
using BenchmarkEF.Infraestructure.Repositories;

namespace BenchmarkEF.Console.Services;

[RankColumn]
[MemoryDiagnoser]
public class BenchmarkEFService
{
    private readonly IMapper _mapper;

    private readonly IBenchmarkEFRepository _benchmarkEFRepository;
    public BenchmarkEFService()
    {
        _benchmarkEFRepository = new BenchmarkEFRepository();

        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        _mapper = config.CreateMapper();
    }

    // ========= EF LINQ ==========
    [Benchmark]
    public List<FunctionaryProjectDto> GetFunctionariesWithProjectsEFLinq()
    {
        var query = _benchmarkEFRepository.GetFunctionariesWithProjectsEFLinq().ToList();

        return _mapper.Map<List<FunctionaryProjectDto>>(query);
    }

    // ========= DAPPER ==========
    [Benchmark]
    public List<FunctionaryProjectDto> GetFunctionariesWithProjectsDapper()
    {
        var query = _benchmarkEFRepository.GetFunctionariesWithProjectsDapper().ToList();

        return _mapper.Map<List<FunctionaryProjectDto>>(query);
    }

    // ========= EF SQL RAW ==========
    [Benchmark]
    public List<FunctionaryProjectDto> GetFunctionariesWithProjectsEFSqlRaw()
    {
        var query = _benchmarkEFRepository.GetFunctionariesWithProjectsEFSqlRaw().ToList();

        return _mapper.Map<List<FunctionaryProjectDto>>(query);
    }

    // ========= DAPPER – RESOURCE ==========
    [Benchmark]
    public List<FunctionaryProjectDto> GetFunctionariesWithProjectsDapperFileResource()
    {
        var query = _benchmarkEFRepository.GetFunctionariesWithProjectsDapperFileResource().ToList();

        return _mapper.Map<List<FunctionaryProjectDto>>(query);
    }

    // ========= EF SQL RAW – RESOURCE ==========
    [Benchmark]
    public List<FunctionaryProjectDto> GetFunctionariesWithProjectsEFSqlRawFileResource()
    {
        var query = _benchmarkEFRepository.GetFunctionariesWithProjectsEFSqlRawFileResource().ToList();

        return _mapper.Map<List<FunctionaryProjectDto>>(query);
    }

    // ========= DAPPER – VIEW ==========
    [Benchmark]
    public List<FunctionaryProjectDto> GetFunctionariesWithProjectsDapperView()
    {
        var query = _benchmarkEFRepository.GetFunctionariesWithProjectsDapperView().ToList();

        return _mapper.Map<List<FunctionaryProjectDto>>(query);
    }

    // ========= EF SQL RAW – VIEW ==========
    [Benchmark]
    public List<FunctionaryProjectDto> GetFunctionariesWithProjectsEFSqlRawView()
    {
        var query = _benchmarkEFRepository.GetFunctionariesWithProjectsEFSqlRawView().ToList();

        return _mapper.Map<List<FunctionaryProjectDto>>(query);
    }
}

