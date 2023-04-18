using Serilog;

namespace GPTServer.Web.WireUp;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureAppConfiguration(app =>
                {
                    app.AddJsonFile("appsettings.json");
                })
                .UseSerilog()
                .Build()
                .Run();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            throw;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
