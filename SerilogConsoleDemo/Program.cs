using Microsoft.Extensions.Configuration;
using Serilog;

namespace SerilogConsoleDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())                                    
                  .AddJsonFile("appsettings.json")
                  .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.WithCaller()    // Careful reflection!!!
                .CreateLogger();


            Log.Logger.Information("Start");
            try
            {
                Run();
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, "Something went wrong!");
            }
            finally
            {
                Log.Logger.Information("End");
                Log.CloseAndFlush();
            }


        }

        public static void Run()
        {
            int a = 10, b = 0;
            Log.Logger.Verbose("Verbose");
            Log.Logger.Debug("Debug");
            Log.Logger.Information("Information");
            Log.Logger.Warning("Warning");
            Log.Logger.Error("Error");
            Log.Logger.Fatal("Fatal");
            Test(1, 1);

            Log.Debug("Dividing {A} by {B}", a, b);
            Console.WriteLine(a / b);
        }

        public static void Test(int a, int b)
        {
            Log.Logger.Information("In Test with {a} {b}", a, b);
        }
    }
}
