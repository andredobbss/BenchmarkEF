using BenchmarkDotNet.Running;
using BenchmarkEF.Console.Services;
using BenchmarkEF.Infraestructure.Resources;
using Microsoft.Data.SqlClient;

class Program
{
    static void Main()
    {
        Console.Title = BenchmarkEFResources.Title;
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        while (true)
        {
            ShowMenu();

            var option = Console.ReadLine()?.Trim();

            switch (option)
            {
                case "1":
                    CreateAndPopularizeDatabase();
                    break;

                case "2":
                    RunBenchmarks();
                    break;

                case "0":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"\n{BenchmarkEFResources.Exit}");
                    Console.ResetColor();
                    return;

                default:
                    ShowError(BenchmarkEFResources.InvalidOption);
                    break;
            }

            Console.WriteLine($"\n{BenchmarkEFResources.PressAnyKey}");
            Console.ReadKey();
            Console.Clear();
        }
    }

    static void ShowMenu()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("==========================================");
        Console.WriteLine(BenchmarkEFResources.Title);
        Console.WriteLine("==========================================");
        Console.ResetColor();

        Console.WriteLine(BenchmarkEFResources.Option1Menu);
        Console.WriteLine(BenchmarkEFResources.Option2Menu);
        Console.WriteLine(BenchmarkEFResources.Option0Menu);
        Console.Write($"\n{BenchmarkEFResources.SelectOption} ");
    }

    static void CreateAndPopularizeDatabase()
    {
        try
        {
            Console.WriteLine($"\n{BenchmarkEFResources.CreateAndPopulatingDB}");
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();

            var service = new PersistenceService();
            service.AddData();

            stopwatch.Stop();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{BenchmarkEFResources.DatabaseCreated} ({stopwatch.Elapsed.TotalSeconds:F1}s)");
            Console.ResetColor();
        }
        catch (SqlException ex)
        {
            ShowError($"{BenchmarkEFResources.SQLError} {ex.Message}");
        }
        catch (Exception ex)
        {
            ShowError($"{BenchmarkEFResources.UnexpectedError} {ex.Message}");
        }
    }

    static void RunBenchmarks()
    {
        try
        {
            Console.WriteLine($"\n{BenchmarkEFResources.CheckingDatabase}");

            var service = new DatabaseValidatorService();

            if (!service.HasDatabase())
            {
                ShowError(BenchmarkEFResources.DataBaseExists);
                return;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(BenchmarkEFResources.StartingBenchmarks);
            Console.ResetColor();

            BenchmarkRunner.Run<BenchmarkEFService>();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n{BenchmarkEFResources.BenchmarksCompleted}");
            Console.ResetColor();
        }
        catch (SqlException ex)
        {
            ShowError($"{BenchmarkEFResources.SQLError} {ex.Message}");
        }
        catch (Exception ex)
        {
            ShowError($"{BenchmarkEFResources.UnexpectedError} {ex.Message}");
        }
    }

    static void ShowError(string mensagem)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\n❌ " + mensagem);
        Console.ResetColor();
    }
}

