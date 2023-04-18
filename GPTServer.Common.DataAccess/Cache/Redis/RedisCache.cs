using GPTServer.Common.DataAccess.Cache.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace GPTServer.Common.DataAccess.Cache.Redis
{
    public class RedisCache<T> : ICache<T> where T : new()
    {
        private readonly TimeSpan _defaultLazyCacheExpiration = TimeSpan.FromDays(999);
        private readonly IDistributedCache _cache;

        public RedisCache(IDistributedCache cache)
        {
            _cache = cache;
        }

        public void AddOrUpdate(
            string cacheId,
            Func<T> loadFunc)
        {
            var item = loadFunc();
            string stringData = JsonSerializer.Serialize(item);

            _cache.SetString(cacheId, stringData, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = _defaultLazyCacheExpiration
            });
        }

        public void AddOrUpdate(
            string cacheId,
            Func<T> loadFunc,
            TimeSpan timeToLive)
        {
            var item = loadFunc();
            string stringData = JsonSerializer.Serialize(item);

            _cache.SetString(cacheId, stringData, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = timeToLive
            });
        }

        public void AddOrUpdate(
            string cacheId,
            T item)
        {
            string stringData = JsonSerializer.Serialize(item);

            _cache.SetString(cacheId, stringData, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = _defaultLazyCacheExpiration
            });
        }

        public void AddOrUpdate(
            string cacheId,
            T item,
            TimeSpan timeToLive)
        {
            string stringData = JsonSerializer.Serialize(item);

            _cache.SetString(cacheId, stringData, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = timeToLive
            });
        }

        public async Task AddOrUpdateAsync(
            string cacheId,
            Func<Task<T>> loadFuncAsync)
        {
            var item = await loadFuncAsync();
            string stringData = JsonSerializer.Serialize(item);
            await _cache.SetStringAsync(cacheId, stringData, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = _defaultLazyCacheExpiration
            });
        }

        public async Task AddOrUpdateAsync(
            string cacheId,
            Func<Task<T>> loadFuncAsync,
            TimeSpan timeToLive)
        {
            var item = await loadFuncAsync();
            string stringData = JsonSerializer.Serialize(item);
            await _cache.SetStringAsync(cacheId, stringData, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = timeToLive
            });
        }

        public async Task AddOrUpdateAsync(
            string cacheId,
            T item)
        {
            string stringData = JsonSerializer.Serialize(item);
            await _cache.SetStringAsync(cacheId, stringData, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = _defaultLazyCacheExpiration
            });
        }

        public async Task AddOrUpdateAsync(
            string cacheId,
            T item,
            TimeSpan timeToLive)
        {
            string stringData = JsonSerializer.Serialize(item);
            await _cache.SetStringAsync(cacheId, stringData, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = timeToLive
            });
        }

        public T GetOrAdd(
            string cacheId,
            Func<T> loadFunc)
        {
            string cacheString = _cache.GetString(cacheId);

            if (string.IsNullOrWhiteSpace(cacheString))
            {
                AddOrUpdate(cacheId, loadFunc, _defaultLazyCacheExpiration);
                cacheString = _cache.GetString(cacheId);
            }

            return JsonSerializer.Deserialize<T>(cacheString);
        }

        public T GetOrAdd(
            string cacheId,
            Func<T> loadFunc,
            TimeSpan timeToLive)
        {
            string cacheString = _cache.GetString(cacheId);

            if (string.IsNullOrWhiteSpace(cacheString))
            {
                AddOrUpdate(cacheId, loadFunc, timeToLive);
                cacheString = _cache.GetString(cacheId);
            }

            return JsonSerializer.Deserialize<T>(cacheString);
        }

        public T Get(string cacheId)
        {
            string cacheString = _cache.GetString(cacheId);

            if (string.IsNullOrWhiteSpace(cacheString))
            {
                return default;
            }

            return JsonSerializer.Deserialize<T>(cacheString);
        }

        public async Task<T> GetAsync(string cacheId)
        {
            string cacheString = await _cache.GetStringAsync(cacheId);

            if (string.IsNullOrWhiteSpace(cacheString))
            {
                return default;
            }

            return JsonSerializer.Deserialize<T>(cacheString);
        }

        public T GetOrAdd(
            string cacheId,
            T item)
        {
            string cacheString = _cache.GetString(cacheId);

            if (string.IsNullOrWhiteSpace(cacheString))
            {
                AddOrUpdate(cacheId, item, _defaultLazyCacheExpiration);
                cacheString = _cache.GetString(cacheId);
            }

            return JsonSerializer.Deserialize<T>(cacheString);
        }

        public T GetOrAdd(
            string cacheId,
            T item,
            TimeSpan timeToLive)
        {
            string cacheString = _cache.GetString(cacheId);

            if (string.IsNullOrWhiteSpace(cacheString))
            {
                AddOrUpdate(cacheId, item, timeToLive);
                cacheString = _cache.GetString(cacheId);
            }

            return JsonSerializer.Deserialize<T>(cacheString);
        }

        public async Task<T> GetOrAddAsync(
            string cacheId,
            Func<Task<T>> loadFuncAsync)
        {
            string cacheString = await _cache.GetStringAsync(cacheId);

            if (string.IsNullOrWhiteSpace(cacheString))
            {
                await AddOrUpdateAsync(cacheId, loadFuncAsync, _defaultLazyCacheExpiration);
                cacheString = await _cache.GetStringAsync(cacheId);
            }

            return JsonSerializer.Deserialize<T>(cacheString);
        }

        public async Task<T> GetOrAddAsync(
            string cacheId,
            Func<Task<T>> loadFuncAsync,
            TimeSpan timeToLive)
        {
            string cacheString = await _cache.GetStringAsync(cacheId);

            if (string.IsNullOrWhiteSpace(cacheString))
            {
                await AddOrUpdateAsync(cacheId, loadFuncAsync, timeToLive);
                cacheString = await _cache.GetStringAsync(cacheId);
            }

            return JsonSerializer.Deserialize<T>(cacheString);
        }

        public async Task<T> GetOrAddAsync(
            string cacheId,
            T item)
        {
            string cacheString = await _cache.GetStringAsync(cacheId);

            if (string.IsNullOrWhiteSpace(cacheString))
            {
                await AddOrUpdateAsync(cacheId, item, _defaultLazyCacheExpiration);
                cacheString = await _cache.GetStringAsync(cacheId);
            }

            return JsonSerializer.Deserialize<T>(cacheString);
        }

        public async Task<T> GetOrAddAsync(
            string cacheId,
            T item,
            TimeSpan timeToLive)
        {
            string cacheString = await _cache.GetStringAsync(cacheId);

            if (string.IsNullOrWhiteSpace(cacheString))
            {
                await AddOrUpdateAsync(cacheId, item, timeToLive);
                cacheString = await _cache.GetStringAsync(cacheId);
            }

            return JsonSerializer.Deserialize<T>(cacheString);
        }

        public void InvalidateCache(string cacheId) =>
            _cache.Remove(cacheId);
    }
}
