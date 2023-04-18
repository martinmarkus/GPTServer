using GPTServer.Common.Core.DataObjects;
using System.Data;

namespace GPTServer.Common.DataAccess.Transactions
{
    public interface IDatabaseTransaction
    {
        Task<DatabaseTransactionResult> RunAsync(
            Func<Task> process,
            IsolationLevel isolationLevel = IsolationLevel.ReadUncommitted);
    }
}
