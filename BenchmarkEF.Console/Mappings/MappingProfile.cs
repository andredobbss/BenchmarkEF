using AutoMapper;
using BenchmarkEF.Console.DTOs;
using BenchmarkEF.Domain.Entities;

namespace BenchmarkEF.Console.Mappings;

internal class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<FunctionaryProjectDomain, FunctionaryProjectDto>().ReverseMap();
    }
}
