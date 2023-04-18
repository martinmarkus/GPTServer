using System;
using System.Threading.Tasks;

namespace GPTServer.Common.DataAccess.Cache.Interfaces
{
    public interface ICache<T> where T : new()
    {
        void AddOrUpdate(string cacheId, Func<T> loadFunc);
        void AddOrUpdate(string cacheId, Func<T> loadFunc, TimeSpan timeToLive);

        void AddOrUpdate(string cacheId, T item);
        void AddOrUpdate(string cacheId, T item, TimeSpan timeToLive);

        Task AddOrUpdateAsync(string cacheId, Func<Task<T>> loadFuncAsync);
        Task AddOrUpdateAsync(string cacheId, Func<Task<T>> loadFuncAsync, TimeSpan timeToLive);

        Task AddOrUpdateAsync(string cacheId, T item);
        Task AddOrUpdateAsync(string cacheId, T item, TimeSpan timeToLive);

        T Get(string cacheId);

        Task<T> GetAsync(string cacheId);

        T GetOrAdd(string cacheId, Func<T> loadFunc);
        T GetOrAdd(string cacheId, Func<T> loadFunc, TimeSpan timeToLive);

        T GetOrAdd(string cacheId, T item);
        T GetOrAdd(string cacheId, T item, TimeSpan timeToLive);

        Task<T> GetOrAddAsync(string cacheId, Func<Task<T>> loadFuncAsync);
        Task<T> GetOrAddAsync(string cacheId, Func<Task<T>> loadFuncAsync, TimeSpan timeToLive);

        Task<T> GetOrAddAsync(string cacheId, T item);
        Task<T> GetOrAddAsync(string cacheId, T item, TimeSpan timeToLive);

        void InvalidateCache(string cacheId);
    }
}
