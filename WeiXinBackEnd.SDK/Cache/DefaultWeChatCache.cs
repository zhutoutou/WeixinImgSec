using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace WeiXinBackEnd.SDK.Cache
{
    public class DefaultWeChatCache:IWeChatCache
    {
        private readonly MemoryCache _cache;

        public DefaultWeChatCache(MemoryCache cache)
        {
            _cache = cache;
        }

        public T Get<T>(string key)
        {
            return _cache.Get<T>(key);
        }

        public async Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> createFactory)
        {
            return await GetOrCreateAsync(key,createFactory,offset:new DateTimeOffset());
        }

        public async Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> createFactory, DateTimeOffset offset)
        {
            return await _cache.GetOrCreateAsync(key,async entry =>
            {
                var result = await createFactory();
                entry.AbsoluteExpiration = offset;
                return result;
            });
        }
    }
}