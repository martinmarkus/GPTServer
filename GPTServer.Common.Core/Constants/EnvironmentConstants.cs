namespace GPTServer.Common.Core.Constants;

public static class EnvironmentConstants
{
    public const string DEVELOPMENT = "Development";
    public const string PRODUCTION = "Production";
    public const string TEST = "Test";

    public const string DEV_FOR_LOGGING = "Dev";
    public const string PROD_FOR_LOGGING = "Prod";
    public const string TEST_FOR_LOGGING = "Test";

    // INFO: Space at the end is not typo.
    public const string ENVIRONMENT_VARIABLE_KEY = "ASPNETCORE_ENVIRONMENT";
}