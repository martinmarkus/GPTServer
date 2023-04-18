using GPTServer.Common.Core.Constants;
using GPTServer.Common.Core.Environment;
using Microsoft.Extensions.Configuration;

namespace GPTServer.Common.Core.Configurations;

public static class StaticConfigOption
{
    public static string GetFromEnvironmentVariable() =>
        System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? string.Empty;

    public static T Get<T>()
    {
        var builder = new ConfigurationBuilder()
            .AddJsonFile(
               $"appsettings.json",
               optional: false,
               reloadOnChange: true);

        string env = EnvironmentMeta.EnvironmentName;

        if (string.IsNullOrEmpty(env))
        {
            builder.AddJsonFile(
               $"appsettings.{EnvironmentConstants.DEVELOPMENT}.json",
               optional: false,
               reloadOnChange: true);
        }
        else if (env.Equals(EnvironmentConstants.DEVELOPMENT)
            || env.Equals(EnvironmentConstants.TEST)
            || env.Equals(EnvironmentConstants.PRODUCTION))
        {
            builder.AddJsonFile(
                 $"appsettings.{env}.json",
                 optional: false,
                 reloadOnChange: true);
        }
        else
        {
            throw new Exception("No usable appsettings file was found.");
        }

        var root = builder.Build();
        var section = root.GetSection(typeof(T).Name);
        var result = section.Get<T>();
        return result;
    }
}