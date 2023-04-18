using GPTServer.Common.Core.Configurations;
using GPTServer.Common.Core.Constants;
using GPTServer.Common.Core.Environment;
using GPTServer.Common.Core.Models;
using GPTServer.Common.DataAccess.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace GPTServer.Common.DataAccess.DbContexts
{
    public class GPTDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<ApiKey> ApiKeys { get; set; }

        // INFO: Private fields
        private readonly string _environmentName;
        private readonly BaseOptions _baseOptions;
        //private readonly string _environmentName = StaticConfigOption.GetFromEnvironmentVariable();

        // INFO: Constructor for DI
        public GPTDbContext(
            DbContextOptions<GPTDbContext> dbContextOptions,
            IOptions<BaseOptions> options) : base(dbContextOptions)
        {
            _baseOptions = options.Value;
            _environmentName = EnvironmentMeta.EnvironmentName;
        }

        // INFO: Constructor for IDesignTimeDbContextFactory implementation
        public GPTDbContext(DbContextOptions<GPTDbContext> dbContextOptions) : base(dbContextOptions)
        {
            _environmentName = EnvironmentMeta.EnvironmentName;
            _baseOptions = StaticConfigOption.Get<BaseOptions>();

            // INFO: Turn off lazy loading
            ChangeTracker.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // INFO: Do not init db as new, if called from BackgroundOperator.
            if (_baseOptions.AppName.Equals(ApplicationNameConstants.Web))
            {
                return;
            }

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasMany(x => x.ApiKeys)
                .WithOne(x => x.User)
                .OnDelete(DeleteBehavior.Cascade);

            // Define indexes
            modelBuilder.DefineIndexes();

            // Add initial data
            modelBuilder.AddInitialData();

            // INFO: Add test data
            modelBuilder.AddTestData();
        }
    }
}
