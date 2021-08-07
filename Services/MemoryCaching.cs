using System;
using System.Threading.Tasks;
using Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace Services
{
    /// <summary>
    ///     内存缓存
    /// </summary>
    public class MemoryCaching : ICachingServices
    {
        /// <summary>
        ///     微软内存缓存接口
        /// </summary>
        private readonly IMemoryCache _memoryCache;

        public MemoryCaching(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public bool Delete(string key)
        {
            _memoryCache.Remove(key);
            return Exist(key);
        }

        public async Task<bool> DeleteAsync(string key)
        {
            return await Task.FromResult(Delete(key));
        }

        public bool Exist(string key)
        {
            var result = Get(key);
            return result != null;
        }

        public async Task<bool> ExistAsync(string key)
        {
            return await Task.FromResult(Exist(key));
        }

        public T Get<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }

        public string Get(string key)
        {
            var result = _memoryCache.Get(key);
            if (result == null)
                return null;
            if (result is string str)
                return str;
            return JsonConvert.SerializeObject(result);
        }

        public async Task<T> GetAsync<T>(string key)
        {
            return await Task.FromResult(Get<T>(key));
        }

        public async Task<string> GetAsync(string key)
        {
            return await Task.FromResult(Get(key));
        }

        public bool Set(string key, string value)
        {
            return Set(key, value, 3600);
        }

        public bool Set(string key, string value, int expirationTime, bool isSlidingExpiration = false)
        {
            if (isSlidingExpiration)
                _memoryCache.Set(key, value,
                    new MemoryCacheEntryOptions {SlidingExpiration = TimeSpan.FromSeconds(expirationTime)});
            else
                _memoryCache.Set(key, value, TimeSpan.FromSeconds(expirationTime));
            return Exist(key);
        }

        public async Task<bool> SetAsync(string key, string value)
        {
            return await Task.FromResult(Set(key, value));
        }

        public async Task<bool> SetAsync(string key, string value, int expirationTime, bool isSlidingExpiration = false)
        {
            return await Task.FromResult(Set(key, value, expirationTime, isSlidingExpiration));
        }
    }
}