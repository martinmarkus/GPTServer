using GPTServer.Common.Core.Models;
using GPTServer.Common.DataAccess.DbContexts;
using GPTServer.Common.DataAccess.Repositores.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GPTServer.Common.DataAccess.Repositores
{
    public abstract class AsyncRepo<T> : IAsyncRepo<T> where T : BaseEntity
    {
        protected readonly HHDbContext _dbContext;

        protected AsyncRepo(HHDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            var existingEntity = await _dbContext.Set<T>()
                .FindAsync(entity.Id);

            if (existingEntity is null)
            {
                var addedEntity = await _dbContext.Set<T>()
                    .AddAsync(entity);

                await SaveChangesAsync();

                return addedEntity.Entity;
            }

            return default;
        }

        public virtual async Task AddRangeAsync(IList<T> entities)
        {
            foreach (var entity in entities)
            {
                await _dbContext
                    .Set<T>()
                    .AddAsync(entity);
            }

            await SaveChangesAsync();
        }

        public virtual async Task<bool> ExistsByIdAsync(Guid id)
        {
            var entity = await _dbContext.Set<T>()
                .FirstOrDefaultAsync(entity => entity.Id == id && !entity.IsDeleted);

            return entity is not null;
        }

        public virtual async Task<IList<T>> GetAllAsync() =>
            await _dbContext.Set<T>()
            .Where(entity => !entity.IsDeleted)
                .OrderBy(entity => entity.CreationDate)
                .ToListAsync();

        public virtual async Task<T> GetByIdAsync(Guid id) =>
            await _dbContext.Set<T>()
                .FirstOrDefaultAsync(entity => entity.Id == id && !entity.IsDeleted);

        public virtual async Task RemoveAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);

            if (entity is not null)
            {
                entity.IsDeleted = true;
                _dbContext.Entry(entity).CurrentValues.SetValues(entity);
                await SaveChangesAsync();
            }
        }

        public virtual async Task RemoveAsync(T entity)
        {
            if (entity is not null)
            {
                entity.IsDeleted = true;
                _dbContext.Entry(entity).CurrentValues.SetValues(entity);
                await SaveChangesAsync();
            }
        }

        public virtual async Task RemoveAllAsync(IList<T> entities)
        {
            if (entities is not null)
            {
                _dbContext.RemoveRange(entities);

                foreach (var entity in entities)
                {
                    RemoveWithoutSave(entity);
                }

                await SaveChangesAsync();
            }
        }

        public virtual async Task RemoveAllAsync(IList<Guid> entityIds)
        {
            var existingEntities = await _dbContext
                .Set<T>()
                .Where(x => entityIds.Contains(x.Id))
                .ToListAsync();

            if (existingEntities is null || existingEntities.Count == 0)
            {
                return;
            }

            foreach (var entity in existingEntities)
            {
                entity.IsDeleted = true;
            }

            await UpdateRangeAsync(existingEntities);
        }

        protected virtual void RemoveWithoutSave(T entity)
        {
            if (entity is not null)
            {
                entity.IsDeleted = true;
                _dbContext.Entry(entity).CurrentValues.SetValues(entity);
            }
        }

        public virtual async Task<T> AddOrUpdateAsync(T entity)
        {
            var existingEntity = await _dbContext
                .Set<T>()
                .FindAsync(entity.Id);

            T resultEntity;
            if (existingEntity is not null && !entity.IsDeleted)
            {
                _dbContext.Entry(existingEntity)
                    .CurrentValues
                    .SetValues(entity);

                resultEntity = existingEntity;
            }
            else
            {
                resultEntity = (await _dbContext.AddAsync(entity)).Entity;
            }

            await SaveChangesAsync();

            return resultEntity;
        }

        public virtual async Task AddOrUpdateRangeAsync(IList<T> entities)
        {
            foreach (var entity in entities)
            {
                await AddOrUpdateAsync(entity);
            }
        }

        public virtual async Task UpdateAsync(T entity)
        {
            var existing = await _dbContext
                .Set<T>()
                .FindAsync(entity.Id);

            if (existing is not null && !entity.IsDeleted)
            {
                _dbContext.Entry(existing)
                    .CurrentValues
                    .SetValues(entity);

                await SaveChangesAsync();
            }
        }

        public virtual async Task UpdateRangeAsync(IList<T> entities)
        {
            foreach (var entity in entities)
            {
                var existing = await _dbContext
                    .Set<T>()
                    .FindAsync(entity.Id);

                if (existing != null && !entity.IsDeleted)
                {
                    _dbContext.Entry(existing)
                        .CurrentValues
                        .SetValues(entity);
                }
            }

            await SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async Task<long> CountOfAllAsync() =>
            await _dbContext
                .Set<T>()
                .CountAsync();
    }
}
