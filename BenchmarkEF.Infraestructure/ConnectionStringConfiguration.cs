namespace BenchmarkEF.Infraestructure;

internal static class ConnectionStringConfiguration
{
    internal const string databaseName = "BenchmarkEF";
    internal static string GetConnectionString()
    {
        string sqlConnectionString = Environment.GetEnvironmentVariable("DEFAULT_CONNECTION_BENCHMARKEF")!; //??
        //$@"Server = SERVERABC;
        //     Database = databaseName; 
        //     User ID = ;
        //     Password = ;
        //     Trusted_Connection = False;
        //     TrustServerCertificate = True";

        return sqlConnectionString;
    }
}
