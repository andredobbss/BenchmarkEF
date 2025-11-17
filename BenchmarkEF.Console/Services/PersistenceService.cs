using BenchmarkEF.Domain.Entities;
using BenchmarkEF.Domain.Entities.Enums;
using BenchmarkEF.Infraestructure.Context;
using BenchmarkEF.Infraestructure.Resources;
using Bogus;
using Microsoft.EntityFrameworkCore;

namespace BenchmarkEF.Console.Services
{

    public sealed class PersistenceService : IDisposable
    {
        private readonly BenchmarkEFContext _context;

        public PersistenceService()
        {
            _context = new BenchmarkEFContext();
        }

        public void AddData()
        {
            _context.Database.EnsureDeleted();

            _context.Database.EnsureCreated();

            var departments = new Faker<Department>()
                 .RuleFor(d => d.DepartmentName, f => f.Commerce.Department())
                 .RuleFor(d => d.Description, f => f.Commerce.ProductDescription())
                 .Generate(10);
            _context.BulkInsert(departments);
            _context.SaveChanges();

            var functionaries = new Faker<Functionary>()
                .RuleFor(f => f.FunctionaryName, f => f.Name.FullName())
                .RuleFor(f => f.Position, f => f.Name.JobTitle())
                .RuleFor(f => f.Salary, f => f.Finance.Amount(3000, 10000))
                .RuleFor(f => f.HiringDate, f => f.Date.SoonDateOnly())
                .RuleFor(f => f.DepartmentId, f => f.PickRandom(departments).DepartmentId)
                .Generate(50);
            _context.BulkInsert(functionaries);
            _context.SaveChanges();

            var detailsFaker = new Faker<FunctionaryDetail>()
                .RuleFor(fd => fd.Address, f => f.Address.FullAddress())
                .RuleFor(fd => fd.PhoneNumber, f => f.Phone.PhoneNumber("(##) ####-####"))
                .RuleFor(fd => fd.CPF, f => f.Random.Int(100000000, 999999999).ToString())
                .RuleFor(fd => fd.DateOfBirth, f => f.Date.PastDateOnly(40))
                .RuleFor(fd => fd.Gender, f => f.PickRandom(new[]
                {
                    Gender.Male,
                    Gender.Female
                }))
                .RuleFor(fd => fd.MaritalStatus, f => f.PickRandom(new[]
                {
                    MaritalStatus.Single,
                    MaritalStatus.Married,
                    MaritalStatus.Divorced,
                    MaritalStatus.Widowed
                }))
                .RuleFor(fd => fd.Education, f => f.PickRandom(new[]
                {
                    Education.None,
                    Education.Primary,
                    Education.Secondary,
                    Education.Bachelor,
                    Education.Master,
                    Education.Doctorate
                }));
            var functionaryDetails = functionaries.Select(f =>
            {
                var detail = detailsFaker.Generate();
                detail.FunctionaryId = f.FunctionaryId;
                return detail;
            }).ToList();
            _context.BulkInsert(functionaryDetails);
            _context.SaveChanges();

            var projects = new Faker<Project>()
                .RuleFor(p => p.ProjectName, f => f.Commerce.ProductName())
                .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
                .RuleFor(p => p.Budget, f => f.Finance.Amount(10000, 500000))
                .RuleFor(p => p.StartDate, f => f.Date.Past(2))
                .RuleFor(p => p.UpdateDate, (f, p) => f.Date.Between(p.StartDate, DateTime.Now))
                .RuleFor(p => p.EndDate, (f, p) => f.Date.Future(2, p.StartDate))
                .RuleFor(p => p.Status, f => f.PickRandom(new[]
                {
                   ProjectStatus.NotStarted,
                   ProjectStatus.InProgress,
                   ProjectStatus.Completed,
                   ProjectStatus.OnHold
                }))
                .Generate(5000);
            _context.BulkInsert(projects);
            _context.SaveChanges();

            var usedPairs = new HashSet<(int FunctionaryId, int ProjectId)>();
            var rnd = new Random();

            var functionaryProjects = new List<FunctionaryProject>();

            while (functionaryProjects.Count < 8000)
            {
                var f = functionaries[rnd.Next(functionaries.Count)];
                var p = projects[rnd.Next(projects.Count)];

                var pair = (f.FunctionaryId, p.ProjectId);

                if (usedPairs.Contains(pair))
                    continue;

                usedPairs.Add(pair);

                functionaryProjects.Add(new FunctionaryProject
                {
                    FunctionaryId = f.FunctionaryId,
                    ProjectId = p.ProjectId
                });
            }
            _context.BulkInsert(functionaryProjects);
            _context.SaveChanges();

            CreateView();
        }

        private void CreateView()
        {
            var sql = BenchmarkEFResources.vw_FunctionaryProject;
            _context.Database.ExecuteSqlRaw(sql);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
