using GPTServer.Common.Core.Constants;
using GPTServer.Common.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace GPTServer.Common.DataAccess.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void AddInitialData(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    Email = "teszt@aichatmester.hu",
                    PasswordHash = "M+QCWZJLLRFCKU0V/PUERW4F21H8V3DTTJTPDDXVLGA=",
                    PasswordSalt = "SDA6HQ+8CYIJ+9OM23GM9KJDVGYOIP+TJ9SSAAN9TWM09PXVPINP/OL38JDIPRQQHAKXWONR1TESEM05XTRLPKRBY2QBSW/1IXIFMGWP91HPIQP0F2A1WHGQHTMCX10W",
                    HasExtensionPermission = true,
                    LastAuthRoutingEnv = RoutingEnvironmentConstants.ChatGPTExtension,
                    UniqueId = Guid.NewGuid().ToString()
				});
        }

        public static void AddTestData(this ModelBuilder modelBuilder)
        {
        }

        public static void DefineIndexes(this ModelBuilder modelBuilder)
        {
            // INFO: ApiKey
            modelBuilder.AddNonClusteredIndex<ApiKey>(nameof(ApiKey.IsDeleted), nameof(ApiKey.Id));
            modelBuilder.AddNonClusteredIndex<ApiKey>(nameof(ApiKey.IsDeleted), nameof(ApiKey.UserId));
            modelBuilder.AddNonClusteredIndex<ApiKey>(nameof(ApiKey.IsDeleted), nameof(ApiKey.UserId), nameof(ApiKey.IsActive));
            modelBuilder.AddNonClusteredIndex<ApiKey>(nameof(ApiKey.IsDeleted), nameof(ApiKey.UserId), nameof(ApiKey.Key));

            // INFO: User
            modelBuilder.AddNonClusteredIndex<User>(nameof(User.IsDeleted), nameof(User.Id));
            modelBuilder.AddNonClusteredIndex<User>(nameof(User.IsDeleted), nameof(User.Email));
        }

        private static void AddNonClusteredIndex<T>(
           this ModelBuilder modelBuilder,
           params string[] columnNames)
           where T : BaseEntity =>
            modelBuilder.Entity<T>()
                .HasIndex(columnNames)
                .IsClustered(false)
                .IsUnique(false);
    }
}
