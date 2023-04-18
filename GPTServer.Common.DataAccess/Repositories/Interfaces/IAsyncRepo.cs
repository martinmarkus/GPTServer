using GPTServer.Common.Core.Models;

namespace GPTServer.Common.DataAccess.Repositores.Interfaces
{
    public interface IAsyncRepo<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(Guid id);

        Task<IList<T>> GetAllAsync();

        Task<T> AddAsync(T entity);

        Task AddRangeAsync(IList<T> entity);

        Task UpdateAsync(T entity);

        Task UpdateRangeAsync(IList<T> entities);

        Task AddOrUpdateRangeAsync(IList<T> entities);

        Task<T> AddOrUpdateAsync(T entity);

        Task RemoveAsync(Guid id);

        Task RemoveAsync(T entity);

        Task RemoveAllAsync(IList<T> entities);

        Task RemoveAllAsync(IList<Guid> entityIds);

        Task<long> CountOfAllAsync();

        Task<bool> ExistsByIdAsync(Guid id);

        Task SaveChangesAsync();
    }
}
