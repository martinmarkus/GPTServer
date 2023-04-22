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

// functions:
// - add user (generate custom key, save IP, save GPT API key)
// - update user -||-
// - extension auth: send custom key -> save IP -> return jwt with actual IP claim
// - chatting: with valid JWT