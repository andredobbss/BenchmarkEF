using BenchmarkEF.Domain.Repositories;
using Microsoft.Data.SqlClient;

namespace BenchmarkEF.Infraestructure.Repositories;

public sealed class DatabaseValidatorRepository : IDatabaseValidatorRepository
{
    public bool HasDatabase()
    {
        var masterConnectionString = new SqlConnectionStringBuilder(
           ConnectionStringConfiguration.GetConnectionString())
        {
            InitialCatalog = "master"
        }.ConnectionString;

        using var connection = new SqlConnection(masterConnectionString);

        const string sql = @"SELECT CASE 
                                  WHEN EXISTS (SELECT name FROM sys.databases WHERE name = @db) 
                                  THEN 1 ELSE 0 END";
        connection.Open();
        using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@db", ConnectionStringConfiguration.databaseName);
        var scalarResult = command.ExecuteScalar();
        connection.Close();

        if ((int)scalarResult == 1)
            return true;
        return false;
    }
}
