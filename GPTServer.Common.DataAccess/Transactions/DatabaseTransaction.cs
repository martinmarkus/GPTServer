using GPTServer.Common.Core.DataObjects;
using GPTServer.Common.DataAccess.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace GPTServer.Common.DataAccess.Transactions
{
    public class DatabaseTransaction : IDatabaseTransaction
    {
        private readonly DbContexts.GPTDbContext _dbContext;

        public DatabaseTransaction(DbContexts.GPTDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<DatabaseTransactionResult> RunAsync(
            Func<Task> process,
            IsolationLevel isolationLevel = IsolationLevel.ReadUncommitted)
        {
            bool committed = false;
            Exception exception = default;

            if (process == null)
            {
                throw new ArgumentNullException(nameof(process));
            }

            var executionStrategy = _dbContext.Database.CreateExecutionStrategy();
            await executionStrategy.ExecuteAsync(async () =>
            {
                IDbContextTransaction transaction = default;
                try
                {
                    transaction = await _dbContext.Database.BeginTransactionAsync(isolationLevel);

                    if (transaction == null)
                    {
                        throw new InvalidOperationException("The starting of transaction was unsuccessful.");
                    }

                    await process();
                    await transaction.CommitAsync();

                    committed = true;
                }
                catch (Exception e)
                {
                    exception = e;

                    committed = false;
                    if (transaction != null)
                    {
                        await transaction.RollbackAsync();
                    }
                }
            });

            return new()
            {
                IsSuccess = committed,
                Exception = exception
            };
        }
    }
}
