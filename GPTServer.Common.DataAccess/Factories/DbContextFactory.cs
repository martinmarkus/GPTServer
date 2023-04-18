using GPTServer.Common.Core.Configurations;
using GPTServer.Common.DataAccess.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace GPTServer.Common.DataAccess.Factories
{
    /// <summary>
    /// DbContext factory for migration generating. Supports multiple environments and appsettings options.
    /// </summary>
    public class DbContextFactory : IDesignTimeDbContextFactory<DbContexts.GPTDbContext>
    {
        public DbContexts.GPTDbContext CreateDbContext(string[] args)
        {
            var baseOptions = StaticConfigOption.Get<BaseOptions>();
            var dbOptions = StaticConfigOption.Get<DbOptions>();

            DbContextOptionsBuilder<DbContexts.GPTDbContext> optionsBuilder = new();

            optionsBuilder
                .UseSqlServer(
                    dbOptions.ConnectionString,
                    optionsBuilder =>
                    {
                        optionsBuilder.MigrationsAssembly(dbOptions.MigrationAssembly);
                        optionsBuilder.EnableRetryOnFailure(
                            maxRetryCount: 2,
                            maxRetryDelay: TimeSpan.FromSeconds(1),
                            errorNumbersToAdd: new int[] { });
                    });

            // INFO: Turn off change tracking
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            return new DbContexts.GPTDbContext(optionsBuilder.Options);
        }
    }
}
