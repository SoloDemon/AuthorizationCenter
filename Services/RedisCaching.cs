using System;
using System.Threading.Tasks;
using Interfaces;

namespace Services
{
    public class RedisCaching : ICachingServices
    {
        public bool Delete(string key)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(string key)
        {
            throw new NotImplementedException();
        }

        public bool Exist(string key)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistAsync(string key)
        {
            throw new NotImplementedException();
        }

        public T Get<T>(string key)
        {
            throw new NotImplementedException();
        }

        public string Get(string key)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync<T>(string key)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetAsync(string key)
        {
            throw new NotImplementedException();
        }

        public bool Set(string key, string value)
        {
            throw new NotImplementedException();
        }

        public bool Set(string key, string value, int expirationTime, bool isSlidingExpiration = false)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetAsync(string key, string value)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetAsync(string key, string value, int expirationTime, bool isSlidingExpiration = false)
        {
            throw new NotImplementedException();
        }
    }
}