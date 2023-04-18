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
                   Id = new Guid("b94fb8f4-08b8-48da-91f2-634c83b5c145"),
                   Email = "martinmarkus0@gmail.com",
                   UniqueId = "ABC123",
                   IsDeleted = false,
                   CreationDate = DateTime.Now,
               });
        }

        public static void AddTestData(this ModelBuilder modelBuilder)
        {
        
        }

        public static void DefineIndexes(this ModelBuilder modelBuilder)
        {
            // INFO: Non clustered indexes
            modelBuilder.AddNonClusteredIndex<User>(nameof(User.Email), nameof(User.IsDeleted));
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
