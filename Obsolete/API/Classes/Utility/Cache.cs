using Microsoft.Extensions.Caching.Memory;
using System;
using API.Interfaces;

namespace API.Classes.Utility
{
    public class Cache<T> : ICache<T>
    {
        private readonly IMemoryCache _memoryCache;

        public Cache(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        /// <summary>
        /// Gets the object in memory if found, if not, function will be used to get the object needing to be cached
        /// </summary>
        /// <param name="key">Refers to the cached object, caller must make sure uniqueness.</param>
        /// <param name="ttl">Cache time to live in seconds.</param>
        /// <param name="function">Delegate to get the object to cache.</param>
        /// <returns>Cached object referred to by key</returns>
        public T GetOrSet(string key, int ttl, Func<T> function)
        {
            if (!_memoryCache.TryGetValue(key, out T cached))
            {
                cached = function();

                var opts = new MemoryCacheEntryOptions
                {
                    SlidingExpiration = TimeSpan.FromSeconds(ttl)
                };

                _memoryCache.Set(key, cached, opts);
            }

            return cached;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key">Refers to the cached object, caller must make sure uniqueness.</param>
        /// <param name="ttl">Cache time to live in seconds.</param>
        /// <param name="objectToCache">Object to cache</param>
        /// <returns>True if value is set</returns>>
        public bool Set(string key, int ttl, T objectToCache)
        {

            var valueSet = true;

            try
            {
                var opts = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddSeconds(ttl)
                };

                _memoryCache.Set(key, objectToCache, opts);

            }
            catch
            {
                valueSet = false;
            }

            return valueSet;
        }

        /// <summary>
        /// Expires the cached object based on the key provided
        /// </summary>
        /// <param name="key">Key to expire in the in-memory cache</param>
        public void Expire(string key)
        {
            _memoryCache.Remove(key);
        }
    }
}
