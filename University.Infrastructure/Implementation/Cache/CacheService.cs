using Microsoft.Extensions.Caching.Memory;
using System.Runtime.Caching;
using University.Application.Interfaces;

namespace University.Infrastructure.Implementation.Cache
{
    public class CacheService : ICacheService
    {
        private ObjectCache _memoryCache = System.Runtime.Caching.MemoryCache.Default;
        public T GetData<T>(string key)
        {
            try
            {
                T item = (T)_memoryCache.Get(key);
                return item;
            }
            catch 
            {
                throw new Exception("Error occurred while getting Cached data");
            }
        }
        public object RemoveData(string key)
        {
            var result = true;

            try
            {
                if (!string.IsNullOrEmpty(key))
                {
                    var results = _memoryCache.Remove(key);
                }
                else
                    result = false;
                return result;
            }
            catch 
            {
                
                throw new Exception("Error occurred while removing Cached data");
            }
        }
        public bool SetData<T>(string key, T value, DateTimeOffset expirationDate)
        {
            var result = true;
            try
            {
                if (!string.IsNullOrEmpty(key))
                {
                    _memoryCache.Set(key, value, expirationDate);
                }
                else
                {
                    result = false;
                }

                return result;

            }
            catch 
            {
               
                throw new Exception("Error occurred while Caching data");
            }
        }
    }
}
