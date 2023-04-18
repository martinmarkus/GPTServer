using GPTServer.Common.Core.Constants;

namespace GPTServer.Common.Core.Environment;

public static class EnvironmentMeta
{
    public static string EnvironmentName => System.Environment
        .GetEnvironmentVariable(EnvironmentConstants.ENVIRONMENT_VARIABLE_KEY)
            ?? EnvironmentConstants.DEVELOPMENT;
}