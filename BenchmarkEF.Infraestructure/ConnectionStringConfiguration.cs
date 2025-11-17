namespace BenchmarkEF.Infraestructure;

internal static class ConnectionStringConfiguration
{
    internal const string databaseName = "BenchmarkEF"; // Defina o nome do banco de dados
    internal static string GetConnectionString()
    {
        // Obtenha a string de conexão da variável de ambiente
        string sqlConnectionString = Environment.GetEnvironmentVariable("DEFAULT_CONNECTION_BENCHMARKEF") ??
                   $@"Server = SERVERABC;
                      Database = {databaseName}; 
                      User ID = abc;
                      Password = xxxxxxxx;
                      Trusted_Connection = False;
                      TrustServerCertificate = True";

        return sqlConnectionString;
    }
}
