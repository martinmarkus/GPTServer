using GPTServer.Common.Core.Configurations;
using GPTServer.Common.DataAccess.Cache.Interfaces;
using LazyCache;
using Microsoft.Extensions.Options;

namespace GPTServer.Common.DataAccess.Cache.Lazy
{
    public class LazyCache<T> : ICache<T> where T : new()
    {
        private readonly IAppCache _cache;
        private readonly CachingOptions _cachingOptions;

        public LazyCache(
            IAppCache cache,
            IOptions<CachingOptions> options)
        {
            _cache = cache;
            _cachingOptions = options.Value;
        }

        public void AddOrUpdate(
            string cacheId,
            Func<T> loadFunc) =>
            _cache.Add(
                cacheId,
                item: loadFunc(),
                DateTimeOffset.UtcNow.Add(TimeSpan.FromMinutes(_cachingOptions.DefaultMinutesToLive)));

        public void AddOrUpdate(
            string cacheId,
            Func<T> loadFunc,
            TimeSpan timeToLive) =>
            _cache.Add(
                cacheId,
                item: loadFunc(),
                DateTimeOffset.UtcNow.Add(timeToLive));

        public void AddOrUpdate(
            string cacheId,
            T item) =>
            _cache.Add(
                cacheId,
                item,
                DateTimeOffset.UtcNow.Add(TimeSpan.FromMinutes(_cachingOptions.DefaultMinutesToLive)));

        public void AddOrUpdate(
            string cacheId,
            T item,
            TimeSpan timeToLive) =>
            _cache.Add(
                cacheId,
                item,
                DateTimeOffset.UtcNow.Add(timeToLive));

        public async Task AddOrUpdateAsync(
            string cacheId,
            Func<Task<T>> loadFuncAsync) =>
            _cache.Add(
                cacheId,
                item: await loadFuncAsync(),
                DateTimeOffset.UtcNow.Add(TimeSpan.FromMinutes(_cachingOptions.DefaultMinutesToLive)));

        public async Task AddOrUpdateAsync(
            string cacheId,
            Func<Task<T>> loadFuncAsync,
            TimeSpan timeToLive) =>
            _cache.Add(
                cacheId,
                item: await loadFuncAsync(),
                DateTimeOffset.UtcNow.Add(timeToLive));

#pragma warning disable CS1998
        public async Task AddOrUpdateAsync(
            string cacheId,
            T item) =>
            _cache.Add(
                cacheId,
                item,
                DateTimeOffset.UtcNow.Add(TimeSpan.FromMinutes(_cachingOptions.DefaultMinutesToLive)));

        public async Task AddOrUpdateAsync(
            string cacheId,
            T item,
            TimeSpan timeToLive) =>
            _cache.Add(
                cacheId,
                item,
                DateTimeOffset.UtcNow.Add(timeToLive));
#pragma warning restore CS1998

        public T Get(string cacheId) =>
           _cache.Get<T>(cacheId);

        public async Task<T> GetAsync(string cacheId) =>
            await _cache.GetAsync<T>(cacheId);

        public T GetOrAdd(
           string cacheId,
           Func<T> loadFunc) =>
           _cache.GetOrAdd(
               cacheId,
               loadFunc,
               DateTimeOffset.UtcNow.Add(TimeSpan.FromMinutes(_cachingOptions.DefaultMinutesToLive)));

        public T GetOrAdd(
           string cacheId,
           Func<T> loadFunc,
           TimeSpan timeToLive) =>
           _cache.GetOrAdd(
               cacheId,
               loadFunc,
               expires: DateTimeOffset.UtcNow.Add(timeToLive));

        public T GetOrAdd(
           string cacheId,
           T item) =>
           _cache.GetOrAdd(
               cacheId,
               () => item,
               DateTimeOffset.UtcNow.Add(TimeSpan.FromMinutes(_cachingOptions.DefaultMinutesToLive)));

        public T GetOrAdd(
           string cacheId,
           T item,
           TimeSpan timeToLive) =>
           _cache.GetOrAdd(
               cacheId,
               () => item,
               expires: DateTimeOffset.UtcNow.Add(timeToLive));

        public async Task<T> GetOrAddAsync(
            string cacheId,
            Func<Task<T>> loadFuncAsync) =>
            await _cache.GetOrAddAsync(
                cacheId,
                loadFuncAsync,
                DateTimeOffset.UtcNow.Add(TimeSpan.FromMinutes(_cachingOptions.DefaultMinutesToLive)),
                ExpirationMode.ImmediateEviction);

        public async Task<T> GetOrAddAsync(
            string cacheId,
            Func<Task<T>> loadFuncAsync,
            TimeSpan timeToLive) =>
            await _cache.GetOrAddAsync(
                cacheId,
                loadFuncAsync,
                expires: DateTimeOffset.UtcNow.Add(timeToLive),
                ExpirationMode.ImmediateEviction);

#pragma warning disable CS1998
        public async Task<T> GetOrAddAsync(
            string cacheId,
            T item) =>
            await _cache.GetOrAddAsync(
                cacheId,
                async () => item,
                DateTimeOffset.UtcNow.Add(TimeSpan.FromMinutes(_cachingOptions.DefaultMinutesToLive)),
                ExpirationMode.ImmediateEviction);

        public async Task<T> GetOrAddAsync(
            string cacheId,
            T item,
            TimeSpan timeToLive) =>
            await _cache.GetOrAddAsync(
                cacheId,
                async () => item,
                DateTimeOffset.UtcNow.Add(timeToLive),
                ExpirationMode.ImmediateEviction);
#pragma warning restore CS1998

        public void InvalidateCache(string cacheId) =>
            _cache.Remove(cacheId);
    }
}