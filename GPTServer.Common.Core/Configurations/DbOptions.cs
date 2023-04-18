namespace GPTServer.Common.Core.Configurations;

public class DbOptions
{
    public string ConnectionString { get; set; }

    public string MigrationAssembly { get; set; }

    public bool AutoMigration { get; set; }
}
